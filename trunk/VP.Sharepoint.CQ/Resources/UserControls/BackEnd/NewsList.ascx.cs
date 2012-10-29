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

            if (CurrentMode == SPControlMode.Edit || CurrentMode == SPControlMode.New)
            {
                SPFile file = Utilities.UploadFileToDocumentLibrary(web, fuThumb.PostedFile.FileName, ListsName.InternalName.ResourcesList);
                item[FieldsName.NewsList.InternalName.ImageThumb] = file.Url;

                //file = Utilities.UploadFileToDocumentLibrary(web, fuSmallThumb.FileName, ListsName.InternalName.ResourcesList);
                //item[FieldsName.NewsList.InternalName.ImageSmallThumb] = file.Url;

                //file = Utilities.UploadFileToDocumentLibrary(web, fuImageHot.FileName, ListsName.InternalName.ResourcesList);
                //item[FieldsName.NewsList.InternalName.ImageHot] = file.Url;
            }

            //Save item to list
            
            item[FieldsName.NewsList.InternalName.ImageThumb] = Server.MapPath(fuThumb.FileName);
           
            item[FieldsName.NewsList.InternalName.ImageHot] = fuImageHot.FileName;

            SaveButton.SaveItem(SPContext.Current, false, string.Empty);
        }

        private void BindData()
        {           
            //Bind ddlCategory
            try
            {               
                if (CurrentMode.Equals(SPControlMode.New) || CurrentMode.Equals(SPControlMode.Edit))
                {
                    Utilities.BindToDropDown(CurrentWeb, ddlCategory, ListsName.InternalName.CategoryList, FieldsName.CategoryList.InternalName.CategoryID,
                        FieldsName.CategoryList.InternalName.ParentID, FieldsName.CategoryList.InternalName.Order, FieldsName.CategoryList.InternalName.CategoryLevel);
                }
                if (CurrentMode.Equals(SPControlMode.Edit))
                {
                    ddlCategory.SelectedValue = Convert.ToString(CurrentItem[FieldsName.NewsList.InternalName.NewsGroup]);
                }
                if (CurrentMode.Equals(SPControlMode.Display))
                {                    
                    ddlCategory.Visible = false;
                    lblCatDisplay.Visible = true;
                    lblCatDisplay.Text = ddlCategory.SelectedItem.Text;
                }

            }
            catch (Exception ex)
            {
                Utilities.LogToULS(ex);
            }
        }

        private string GetCatNameByCatId()
        {
            SPQuery query = new SPQuery();
            SPList list = web.Lists.TryGetList(ListsName.DisplayName.CategoryList);
            if (list!=null)
            {
                query.Query = string.Format(@"<Where><Eq><FieldRef Name='{0}'/><Value Type='Text'>{1}</Value></Eq></Where>", FieldsName.CategoryList.InternalName.CategoryID, CurrentItem[FieldsName.NewsList.InternalName.NewsGroup]);
                query.RowLimit = 1;

                SPListItemCollection listItemColection = list.GetItems(query);
                if (listItemColection.Count>0)
                {
                    SPListItem spListItem = listItemColection[0];
                    return Convert.ToString(spListItem[FieldsName.CategoryList.InternalName.ParentName]);
                }
            }
            return string.Empty;
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
