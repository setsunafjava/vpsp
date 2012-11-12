using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class MenuList : BackEndUC, IValidator
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (CurrentMode.Equals(SPControlMode.New))
                {
                    Utilities.BindToDropDown(CurrentWeb, ddlParentName, ListsName.InternalName.MenuList, FieldsName.MenuList.InternalName.MenuID, 
                        FieldsName.MenuList.InternalName.ParentID, FieldsName.MenuList.InternalName.MenuOrder, FieldsName.MenuList.InternalName.MenuLevel);

                    Utilities.BindToDropDown(CurrentWeb, ddlCategory, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID,
                        FieldsName.CategoryList.InternalName.ParentID, FieldsName.CategoryList.InternalName.Order, FieldsName.CategoryList.InternalName.CategoryLevel);
                }
                if (CurrentMode.Equals(SPControlMode.Edit))
                {
                    Utilities.BindToDropDown(CurrentWeb, ddlParentName, ListsName.InternalName.MenuList, FieldsName.MenuList.InternalName.MenuID, 
                        FieldsName.MenuList.InternalName.ParentID, FieldsName.MenuList.InternalName.MenuOrder,
                        FieldsName.MenuList.InternalName.MenuLevel, Convert.ToString(CurrentItem[FieldsName.MenuList.InternalName.MenuID])
                        , Convert.ToString(CurrentItem[FieldsName.MenuList.InternalName.ParentID]));


                    Utilities.BindToDropDown(CurrentWeb, ddlCategory, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID,
                        FieldsName.CategoryList.InternalName.ParentID, FieldsName.CategoryList.InternalName.Order,
                        FieldsName.CategoryList.InternalName.CategoryLevel, Convert.ToString(CurrentItem[FieldsName.MenuList.InternalName.CatID]));


                    hidMenuLevel.Value = Convert.ToString(CurrentItem[FieldsName.MenuList.InternalName.MenuLevel]);
                }
                if (CurrentMode.Equals(SPControlMode.Display))
                {
                    ddlParentName.Visible = false;
                    lblParentNameDsp.Visible = true;
                    lblParentNameDsp.Text = Convert.ToString(CurrentItem[FieldsName.MenuList.InternalName.ParentName]);
                    ddlCategory.Visible = false;
                    lblCatDisplay.Visible = true;
                    lblCatDisplay.Text = Convert.ToString(CurrentItem[FieldsName.MenuList.InternalName.CatName]);
                    hidType.Value = Convert.ToString(CurrentItem[FieldsName.MenuList.InternalName.MenuType]);
                }
            }
        }
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e">EventArgs e</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            SPContext.Current.FormContext.OnSaveHandler += CustomSaveHandler;
            Page.Validators.Add(this);
        }
        #endregion

        /// <summary>
        /// Override sharepoint save method.
        /// Create and temporary save a "create account request". 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomSaveHandler(object sender, EventArgs e)
        {
            //set menuID
            if (CurrentMode.Equals(SPControlMode.New))
            {
                CurrentItem[FieldsName.MenuList.InternalName.MenuID] = Guid.NewGuid();
            }
            //set menulevel
            if (!string.IsNullOrEmpty(ddlParentName.SelectedValue))
            {
                CurrentItem[FieldsName.MenuList.InternalName.MenuLevel] = Utilities.GetMenuLevel(CurrentWeb, ListsName.InternalName.MenuList, 
                    FieldsName.MenuList.InternalName.MenuID, ddlParentName.SelectedValue, FieldsName.MenuList.InternalName.MenuLevel);
            }
            //set menuparent
            CurrentItem[FieldsName.MenuList.InternalName.ParentID] = ddlParentName.SelectedValue;
            CurrentItem[FieldsName.MenuList.InternalName.ParentName] = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.MenuList, 
                FieldsName.MenuList.InternalName.MenuID, ddlParentName.SelectedValue, "Text", FieldsName.MenuList.InternalName.Title);
            //update menulevel for all children
            var newLevel = Utilities.ConvertToInt(Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.MenuList,
                FieldsName.MenuList.InternalName.MenuID, ddlParentName.SelectedValue, "Text", FieldsName.MenuList.InternalName.MenuLevel)) + 1;
            if (CurrentMode.Equals(SPControlMode.Edit) && !newLevel.ToString().Equals(hidMenuLevel.Value))
            {
                Utilities.UpdateChildrenLevel(CurrentWeb, ListsName.InternalName.MenuList, FieldsName.MenuList.InternalName.MenuID,
                    FieldsName.MenuList.InternalName.ParentID, Convert.ToString(CurrentItem[FieldsName.MenuList.InternalName.MenuID]), newLevel + 1,
                    FieldsName.MenuList.InternalName.MenuLevel);
            }
            CurrentItem[FieldsName.MenuList.InternalName.CatID] = ddlCategory.SelectedValue;
            CurrentItem[FieldsName.MenuList.InternalName.CatName] = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID, ddlCategory.SelectedValue, "Text", "Title");
            //Save item to list
            CurrentWeb.AllowUnsafeUpdates = true;
            SaveButton.SaveItem(SPContext.Current, false, string.Empty);
        }
        #region Properties
        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// IsValid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Validate
        /// </summary>
        public void Validate()
        {
            IsValid = true;
        }
        #endregion
    }
}
