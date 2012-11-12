using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class CategoryList : BackEndUC, IValidator
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        SPControlMode formMode;
        SPWeb web;
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {               
                BindData();
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
            SPContext.Current.Web.AllowUnsafeUpdates = true;
           
            if (CurrentMode==SPControlMode.New)
            {
                CurrentItem[FieldsName.CategoryList.InternalName.CategoryID] = Guid.NewGuid();
            }

            //set category level
            if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
            {
                CurrentItem[FieldsName.CategoryList.InternalName.CategoryLevel] = Utilities.GetMenuLevel(CurrentWeb, ListsName.InternalName.CategoryList,
                    FieldsName.CategoryList.InternalName.CategoryID, ddlCategory.SelectedValue, FieldsName.CategoryList.InternalName.CategoryLevel);
            }
            //set category parent
            CurrentItem[FieldsName.CategoryList.InternalName.ParentID] = ddlCategory.SelectedValue;
            CurrentItem[FieldsName.CategoryList.InternalName.ParentName] = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList,
                FieldsName.CategoryList.InternalName.CategoryID, ddlCategory.SelectedValue, "Text", FieldsName.CategoryList.InternalName.Title);
            //update category level for all children
            var newLevel = Utilities.ConvertToInt(Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList,
                FieldsName.CategoryList.InternalName.CategoryID, ddlCategory.SelectedValue, "Text", FieldsName.CategoryList.InternalName.CategoryLevel)) + 1;

            if (CurrentMode.Equals(SPControlMode.Edit) && !newLevel.ToString().Equals(hidLevel.Value))
            {
                Utilities.UpdateChildrenLevel(CurrentWeb, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID,
                    FieldsName.CategoryList.InternalName.ParentID, Convert.ToString(CurrentItem[FieldsName.CategoryList.InternalName.CategoryID]), newLevel + 1,
                    FieldsName.CategoryList.InternalName.CategoryLevel);
            }
            //Save item to list
            SaveButton.SaveItem(SPContext.Current, false, string.Empty);
        }

        private void BindData()
        {
            try
            {
                if (CurrentMode.Equals(SPControlMode.New))
                {
                    Utilities.BindToDropDown(CurrentWeb, ddlCategory, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID,
                        FieldsName.CategoryList.InternalName.ParentID, FieldsName.CategoryList.InternalName.Order, FieldsName.CategoryList.InternalName.CategoryLevel);
                }
                if (CurrentMode.Equals(SPControlMode.Edit))
                {
                    Utilities.BindToDropDown(CurrentWeb, ddlCategory, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID,
                        FieldsName.CategoryList.InternalName.ParentID, FieldsName.CategoryList.InternalName.CategoryLevel,
                        FieldsName.CategoryList.InternalName.CategoryLevel, Convert.ToString(CurrentItem[FieldsName.CategoryList.InternalName.CategoryID])
                        , Convert.ToString(CurrentItem[FieldsName.CategoryList.InternalName.ParentID]));
                    hidLevel.Value = Convert.ToString(CurrentItem[FieldsName.CategoryList.InternalName.CategoryLevel]);
                }
                if (CurrentMode.Equals(SPControlMode.Display))
                {
                    ddlCategory.Visible = false;
                    lblCatDisplay.Visible = true;
                    lblCatDisplay.Text = Convert.ToString(CurrentItem[FieldsName.CategoryList.InternalName.ParentName]);
                    hidType.Value = Convert.ToString(CurrentItem[FieldsName.CategoryList.InternalName.Type]);
                }

            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
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
