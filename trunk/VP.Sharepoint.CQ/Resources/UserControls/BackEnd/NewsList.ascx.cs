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
    public partial class NewsList : BackEndUC, IValidator
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
            web = SPContext.Current.Web;            
            if (!Page.IsPostBack)
            {
                formMode = SPContext.Current.FormContext.FormMode;
                if (formMode == Constants.EditForm || formMode == Constants.NewForm)
                {
                    lblCatDisplay.Visible = false;
                    ddlCategory.Visible = true;
                }
                else
                {
                    lblCatDisplay.Visible = true;
                    ddlCategory.Visible = false;
                }
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
            //Get current item
            var item = SPContext.Current.Item;
            SPContext.Current.Web.AllowUnsafeUpdates = true;
            //Save item to list
            item[FieldsName.NewsList.InternalName.NewsGroup] = ddlCategory.SelectedValue;
            SaveButton.SaveItem(SPContext.Current, false, string.Empty);
        }

        private void BindData()
        {           
            //Bind ddlCategory
            try
            {
                //SPList catList = web.Lists.TryGetList(ListsName.DisplayName.CategoryList);
                //if (catList != null)
                //{
                //    SPListItemCollection items = catList.Items;
                //    foreach (SPListItem item in items)
                //    {
                //        ddlCategory.Items.Add(new ListItem(Convert.ToString(item[FieldsName.CategoryList.InternalName.Title]), Convert.ToString(item[FieldsName.CategoryList.InternalName.CategoryID])));
                //    }
                //}
                ////Set value of control when form mode is Edit or Display
                //if (formMode==Constants.DisplayForm||formMode==Constants.EditForm)
                //{
                //    //SPListItem currentItem = GetCurrentItem();
                //    var currentItem = SPContext.Current.Item;
                //    if (currentItem != null)
                //    {
                //        ddlCategory.SelectedValue = Convert.ToString(currentItem[FieldsName.NewsList.InternalName.NewsGroup]);
                //        lblCatDisplay.Text = ddlCategory.SelectedItem.Text;
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private SPListItem GetCurrentItem()
        //{
        //    try
        //    {
        //        int itemId = 0;
        //        if (Page.Request.QueryString["ID"] != null && Page.Request.QueryString["ID"] != string.Empty)
        //        {
        //            itemId = Convert.ToInt32(Page.Request.QueryString["ID"]);
        //        }
        //        SPList list = web.Lists.TryGetList(ListsName.DisplayName.NewsList);
        //        SPListItem listItem = list.GetItemById(itemId);
        //        return listItem;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return null;
        //}

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
