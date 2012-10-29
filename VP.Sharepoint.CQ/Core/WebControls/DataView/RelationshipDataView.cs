using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace VP.Sharepoint.CQ.Core.WebControls
{
    public class RelationshipDataView : ListDataView
    {
        private SPList childList;
        private int endItemIndex;
        private SPList parentList;
        private const string GroupFieldName = "_Relationship_Lookup_Key_";

        /// <summary>
        ///   Parent list name
        /// </summary>
        public string ParentListName
        {
            get
            {
                var value = ViewState["ParentListName"];
                if (value != null)
                {
                    return (string) value;
                }
                return string.Empty;
            }
            set
            {
                ViewState["ParentListName"] = value;
                parentList = null;
            }
        }

        /// <summary>
        ///   Parent list name
        /// </summary>
        [DefaultValue("Title")]
        public string ParentFieldName
        {
            get
            {
                var value = ViewState["ParentFieldName"];
                if (value != null)
                {
                    return (string) value;
                }
                return "Title";
            }
            set { ViewState["ParentFieldName"] = value; }
        }

        /// <summary>
        ///   Parent sort direction
        /// </summary>
        public ListSortDirection ParentSortDirection
        {
            get
            {
                var value = ViewState["ParentSortDirection"];
                if (value != null)
                {
                    return (ListSortDirection) value;
                }
                return ListSortDirection.Ascending;
            }
            set { ViewState["ParentSortDirection"] = value; }
        }

        /// <summary>
        ///   Parent query where condition
        /// </summary>
        public string ParentWhereCondition
        {
            get
            {
                var value = ViewState["ParentWhereCondition"];
                if (value != null)
                {
                    return (string) value;
                }
                return string.Empty;
            }
            set { ViewState["ParentWhereCondition"] = value; }
        }

        /// <summary>
        ///   Child list name
        /// </summary>
        public string ChildListName
        {
            get
            {
                var value = ViewState["ChildListName"];
                if (value != null)
                {
                    return (string) value;
                }
                return string.Empty;
            }
            set
            {
                ViewState["ChildListName"] = value;
                childList = null;
            }
        }

        /// <summary>
        ///   Relationship field name
        /// </summary>
        public string RelationshipField
        {
            get
            {
                var value = ViewState["RelationshipField"];
                if (value != null)
                {
                    return (string) value;
                }
                return string.Empty;
            }
            set
            {
                ViewState["RelationshipField"] = value;
                childList = null;
            }
        }

        /// <summary>
        ///   Parent list
        /// </summary>
        public SPList ParentList
        {
            get
            {
                if (parentList == null)
                {
                    if (!string.IsNullOrEmpty(ParentListName))
                    {
                        parentList = SPContext.Current.Web.Lists[ParentListName];
                    }
                }
                return parentList;
            }
        }

        /// <summary>
        ///   Child list
        /// </summary>
        public SPList ChildList
        {
            get
            {
                if (childList == null)
                {
                    if (!string.IsNullOrEmpty(ChildListName))
                    {
                        childList = SPContext.Current.Web.Lists[ChildListName];
                    }
                }
                return childList;
            }
        }

        /// <summary>
        ///   Max group items per page
        /// </summary>
        [DefaultValue(5)]
        public int GroupLimit
        {
            get
            {
                var value = ViewState["GroupLimit"];
                if (value != null)
                {
                    return (int) value;
                }
                return 5;
            }
            set { ViewState["GroupLimit"] = value; }
        }

        public override SPList List
        {
            get { return ChildList; }
        }

        private string ParentListItemCollectionPosition
        {
            get
            {
                var value = ViewState["ParentListItemCollectionPosition"] as string;
                return value;
            }
            set { ViewState["ParentListItemCollectionPosition"] = value; }
        }

        private List<DictionaryEntry> ChildListItemCollectionPositions
        {
            get
            {
                var value = ViewState["ListItemCollectionPositions"];
                if (value == null)
                {
                    value = new List<DictionaryEntry>();
                    ViewState["ListItemCollectionPositions"] = value;
                }
                return (List<DictionaryEntry>) value;
            }
        }

        protected override bool SupportAggregationFunctions
        {
            get { return false; }
        }

        protected override int StartItemIndex
        {
            get { return (CurrentPage*GroupLimit) - GroupLimit + 1; }
        }

        protected override int EndItemIndex
        {
            get { return endItemIndex; }
        }

        private ListItemCollectionPositions Positions
        {
            get
            {
                var value = ViewState["Positions"];
                if (value == null)
                {
                    value = new ListItemCollectionPositions();
                    ViewState["Positions"] = value;
                }
                return value as ListItemCollectionPositions;
            }
        }

        protected override void BindDataSource()
        {
            var internalParentFieldName = ParentList.Fields[ParentFieldName].InternalName;

            var internalFields = new List<string> {"ID"};
            var fields = new List<string> {"ID"};

            foreach (var viewField in ViewFields.Cast<BaseFieldRef>().Where(item => !item.IsVirtualField))
            {
                var field = List.Fields[viewField.FieldName];
                viewField.Initialize(field);
                internalFields.Add(field.InternalName);
                fields.Add(viewField.FieldName);
            }

            foreach (var sortField in SortFields.Cast<SortFieldRef>())
            {
                var field = List.Fields[sortField.FieldName];
                sortField.Initialize(field);
                internalFields.Add(field.InternalName);
                fields.Add(sortField.FieldName);
            }

            internalFields = internalFields.Distinct().ToList();
            fields = fields.Distinct().ToList();

            if (Positions.Contains(CurrentPage))
            {
                var position = Positions.First(item => item.Page == CurrentPage);
                foreach (var item in position.Items)
                {
                    SPListItemCollectionPosition childPosition = null;
                    if (item.Value != null)
                    {
                        childPosition = new SPListItemCollectionPosition(item.Value.ToString());
                    }
                    SPListItemCollectionPosition nextChildPosition;
                    BindDataForGroup((int)item.Key, childPosition, fields, internalFields, out nextChildPosition);
                }

                endItemIndex = StartItemIndex + position.Groups - 1;
                if (endItemIndex < StartItemIndex)
                {
                    endItemIndex = StartItemIndex;
                }

                HasNextPage = Positions.Contains(CurrentPage + 1) || !string.IsNullOrEmpty(ParentListItemCollectionPosition);
            }
            else
            {
                var currentPosition = new ListItemCollectionPosition
                                          {
                                              Page = CurrentPage,
                                              Items = new List<DictionaryEntry>()
                                          };

                SPListItemCollectionPosition lastParentPosition = null;
                SPListItemCollectionPosition nextParentPosition = null;
                
                var addedKeys = new List<int>();

                if (!string.IsNullOrEmpty(ParentListItemCollectionPosition))
                {
                    lastParentPosition = new SPListItemCollectionPosition(ParentListItemCollectionPosition);
                    nextParentPosition = lastParentPosition;
                }

                // Fill parent data
                var childListItemCollectionPositions = ChildListItemCollectionPositions;

                // Remove null value
                childListItemCollectionPositions.RemoveAll(item => item.Value == null);

                L0001: // Re get group

                if (childListItemCollectionPositions.Count < GroupLimit &&
                    (CurrentPage == 1 || lastParentPosition != null))
                {
                    var query = new SPQuery();
                    var sb = new StringBuilder();
                    sb.AppendFormat(ParentSortDirection == ListSortDirection.Ascending
                                        ? "<OrderBy><FieldRef Name='{0}' /></OrderBy>"
                                        : "<OrderBy><FieldRef Name='{0}' Ascending='FALSE' /></OrderBy>",
                                    ParentFieldName);
                    sb.Append(ParentWhereCondition);
                    query.Query = sb.ToString();
                    query.RowLimit = (uint) GroupLimit;
                    query.ViewFields = string.Format("<FieldRef Name='{0}' />", internalParentFieldName);
                    query.ListItemCollectionPosition = lastParentPosition;

                    var items = ParentList.GetItems(query);
                    nextParentPosition = items.ListItemCollectionPosition;
                    
                    var index = 0;
                    while (childListItemCollectionPositions.Count < GroupLimit && items.Count > 0)
                    {
                        var item = items[index];
                        childListItemCollectionPositions.Add(new DictionaryEntry(item.ID, null));

                        if (childListItemCollectionPositions.Count == GroupLimit)
                        {
                            var fieldValue = Convert.ToString(item[ParentFieldName]);
                            lastParentPosition = new SPListItemCollectionPosition(string.Format("Paged=TRUE&p_{0}={1}&p_ID={2}",
                                                                               ParentFieldName,
                                                                               SPEncode.UrlEncode(fieldValue), item.ID));
                        }

                        index++;

                        // End of item collection, break while loop
                        if (index == items.Count)
                        {
                            break;
                        }
                    }
                }

                var keys = new ArrayList(childListItemCollectionPositions.Select(item => item.Key).ToList());
                foreach (int key in keys)
                {
                    if (addedKeys.Contains(key))
                    {
                        continue;
                    }

                    var id = key;
                    SPListItemCollectionPosition position = null;
                    SPListItemCollectionPosition childNextPosition;

                    var entry = childListItemCollectionPositions.First(item => item.Key.Equals(id));
                    if (entry.Value != null)
                    {
                        position = new SPListItemCollectionPosition(entry.Value.ToString());
                    }

                    var flag = BindDataForGroup(key, position, fields, internalFields, out childNextPosition);
                    if (flag)
                    {
                        addedKeys.Add(key);

                        var indexOf = childListItemCollectionPositions.FindIndex(item => item.Key.Equals(id));
                        var value = childNextPosition != null ? childNextPosition.PagingInfo : null;
                        childListItemCollectionPositions[indexOf] = new DictionaryEntry(id, value);

                        currentPosition.Items.Add(new DictionaryEntry(key, entry.Value));
                    }
                    else
                    {
                        childListItemCollectionPositions.RemoveAll(item => item.Key.Equals(id));
                    }
                }

                if (childListItemCollectionPositions.Count < GroupLimit && nextParentPosition != null)
                {
                    lastParentPosition = nextParentPosition;
                    goto L0001;
                }

                currentPosition.Groups = addedKeys.Count;
                Positions.Add(currentPosition);

                // Save state
                ParentListItemCollectionPosition = lastParentPosition != null ? lastParentPosition.PagingInfo : null;

                // Mark next page
                HasNextPage = nextParentPosition != null ||
                              childListItemCollectionPositions.Any(item => item.Value != null);

                endItemIndex = StartItemIndex + addedKeys.Count - 1;
                if (endItemIndex < StartItemIndex)
                {
                    endItemIndex = StartItemIndex;
                }
            }

            if (DataSource != null)
            {
                DataSource.Columns.Add("ListId", typeof (string));
                DataSource.Columns.Add("RowIndex", typeof (int));
                for (var i = 0; i < DataSource.Rows.Count; i++)
                {
                    DataSource.Rows[i]["ListId"] = ChildList.ID.ToString();
                    DataSource.Rows[i]["RowIndex"] = i;
                }
            }
            else
            {
                DataSource = new DataTable();
            }

            var groupField = (IGroupFieldRef) GroupFields[0];

            var groupHeaderText = groupField.HeaderText;
            groupField.FieldName = GroupFieldName;
            groupField.HeaderText = groupHeaderText;
        }

        private bool BindDataForGroup(int parentId, SPListItemCollectionPosition position, IEnumerable<string> fields,
                                      IEnumerable<string> internalFields, out SPListItemCollectionPosition nextPosition)
        {
            SPListItem item;
            try
            {
                item = ParentList.GetItemById(parentId);
            }
            catch (ArgumentException)
            {
                nextPosition = null;
                return false;
            }

            return BindDataForGroup(item, position, fields, internalFields, out nextPosition);
        }

        private bool BindDataForGroup(SPListItem item, SPListItemCollectionPosition position, IEnumerable<string> fields,
                                      IEnumerable<string> internalFields, out SPListItemCollectionPosition nextPosition)
        {
            var sb = new StringBuilder();
            foreach (var sortField in SortFields.Cast<SortFieldRef>())
            {
                sb.AppendFormat(sortField.GetSortFieldRef());
            }

            if (string.IsNullOrEmpty(WhereCondition))
            {
                sb.AppendFormat(
                    "<Where><Contains><FieldRef Name='{0}' LookupId='TRUE' /><Value Type='Lookup'>{1}</Value></Contains></Where>",
                    RelationshipField, item.ID);
            }
            else
            {
                var whereCondition = WhereCondition.Replace("<Where>", "").Replace("</Where>", "");
                sb.Append("<Where>");
                sb.Append("<And>");
                sb.AppendFormat(
                    "<Contains><FieldRef Name='{0}' LookupId='TRUE' /><Value Type='Lookup'>{1}</Value></Contains>",
                    RelationshipField, item.ID);
                sb.Append(whereCondition);
                sb.Append("</And>");
                sb.Append("</Where>");
            }

            var query = new SPQuery
                            {
                                RowLimit = (uint) RowLimit,
                                ViewFields = BuildViewFields(internalFields),
                                Query = sb.ToString(),
                                ListItemCollectionPosition = position
                            };

            var items = ChildList.GetItems(query);
            if (items.Count > 0)
            {
                var dt = GetDataTable(items, fields, internalFields);
                dt.Columns.Add(GroupFieldName, typeof(string));
                
                if (DataSource == null)
                {
                    DataSource = dt.Clone();
                }

                foreach (DataRow row in dt.Rows)
                {
                    row[GroupFieldName] = item[ParentFieldName];
                    DataSource.ImportRow(row);
                }

                nextPosition = items.ListItemCollectionPosition;

                return true;
            }

            nextPosition = null;

            return false;
        }

        private DataTable GetDataTable(SPListItemCollection items, IEnumerable<string> fields, IEnumerable<string> internalFields)
        {
            throw new NotImplementedException();
        }

        private string BuildViewFields(IEnumerable<string> internalFields)
        {
            throw new NotImplementedException();
        }

        #region Nested type: ListItemCollectionPosition

        [Serializable]
        public class ListItemCollectionPosition
        {
            public int Page { get; set; }
            public int Groups { get; set; }
            public List<DictionaryEntry> Items { get; set; }
        }

        #endregion

        #region Nested type: ListItemCollectionPositions

        [Serializable]
        public class ListItemCollectionPositions : List<ListItemCollectionPosition>
        {
            public bool Contains(int page)
            {
                return this.Any(item => item.Page == page);
            }
        }

        #endregion
    }
}
