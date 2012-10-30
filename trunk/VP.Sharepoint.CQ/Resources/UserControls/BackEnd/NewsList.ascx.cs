using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Web.UI.WebControls;
using System.Collections.Generic;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (CurrentMode == Constants.EditForm || CurrentMode == Constants.NewForm)
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
            List<string> fileNames = new List<string>();
            if (fuThumb.HasFile)
            {
                var fuThumbName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", Utilities.GetPreByTime(DateTime.Now), fuThumb.FileName);
                SPFile file = Utilities.UploadFileToDocumentLibrary(CurrentWeb, fuThumb.PostedFile.InputStream, string.Format(CultureInfo.InvariantCulture,
                    "{0}/{1}/{2}", CurrentWeb.Url, ListsName.InternalName.NewsImagesList, fuThumbName));
                CurrentItem[FieldsName.NewsList.InternalName.ImageThumb] = file.Url;
                fileNames.Add(fuThumb.FileName);

                SPFieldUrlValue imgDsp = new SPFieldUrlValue();
                imgDsp.Description = CurrentItem.Title;
                var webUrl = CurrentWeb.ServerRelativeUrl;
                if (webUrl.Equals("/"))
                {
                    webUrl = "";
                }
                imgDsp.Url = webUrl + "/" + file.Url;
                CurrentItem[FieldsName.NewsList.InternalName.ImageDsp] = imgDsp;
            }
            if (fuSmallThumb.HasFile)
            {
                var fuSmallThumbName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", Utilities.GetPreByTime(DateTime.Now.AddSeconds(1)), fuSmallThumb.FileName);
                SPFile file = Utilities.UploadFileToDocumentLibrary(CurrentWeb, fuSmallThumb.PostedFile.InputStream, string.Format(CultureInfo.InvariantCulture,
                    "{0}/{1}/{2}", CurrentWeb.Url, ListsName.InternalName.NewsImagesList, fuSmallThumbName));
                CurrentItem[FieldsName.NewsList.InternalName.ImageSmallThumb] = file.Url;
                fileNames.Add(fuSmallThumb.FileName);
            }
            if (fuSmallThumb.HasFile)
            {
                var fuImageHotName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", Utilities.GetPreByTime(DateTime.Now.AddSeconds(1)), fuImageHot.FileName);
                SPFile file = Utilities.UploadFileToDocumentLibrary(CurrentWeb, fuImageHot.PostedFile.InputStream, string.Format(CultureInfo.InvariantCulture,
                    "{0}/{1}/{2}", CurrentWeb.Url, ListsName.InternalName.NewsImagesList, fuImageHotName));
                CurrentItem[FieldsName.NewsList.InternalName.ImageHot] = file.Url;
                fileNames.Add(fuImageHot.FileName);
            }

            CurrentWeb.AllowUnsafeUpdates = true;
            SaveButton.SaveItem(SPContext.Current, false, string.Empty);
            if (fileNames.Count > 0)
            {
                foreach (var fileName in fileNames)
                {
                    try
                    {
                        CurrentWeb.AllowUnsafeUpdates = true;
                        CurrentItem.Attachments.Delete(fileName);
                    }
                    catch (Exception ex)
                    {
                        Utilities.LogToULS(ex);
                    }
                }
                CurrentWeb.AllowUnsafeUpdates = true;
                CurrentItem.SystemUpdate(false);
            }
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
