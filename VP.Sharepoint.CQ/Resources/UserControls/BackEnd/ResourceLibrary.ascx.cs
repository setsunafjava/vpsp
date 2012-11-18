﻿using System;
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
    public partial class ResourceLibrary : BackEndUC, IValidator
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
            CurrentItem[FieldsName.ResourceLibrary.InternalName.CategoryId] = ddlCategory.SelectedValue;
            CurrentItem[FieldsName.ResourceLibrary.InternalName.CategoryName] = Utilities.GetValueByField(CurrentWeb, ListsName.InternalName.CategoryList,
                FieldsName.CategoryList.InternalName.CategoryID, ddlCategory.SelectedValue, "Text", FieldsName.CategoryList.InternalName.Title);            
            CurrentWeb.AllowUnsafeUpdates = true;
            List<string> fileNames = new List<string>();
            if (fuImgThumb.HasFile)
            {
                var fuThumbName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", Utilities.GetPreByTime(DateTime.Now), fuImgThumb.FileName);
                SPFile file = Utilities.UploadFileToDocumentLibrary(CurrentWeb, fuImgThumb.PostedFile.InputStream, string.Format(CultureInfo.InvariantCulture,
                    "{0}/{1}/{2}", WebUrl, ListsName.InternalName.GalleryImagesList, fuThumbName));
                CurrentItem[FieldsName.ResourceLibrary.InternalName.ImgThumb] = file.Url;
                fileNames.Add(fuImgThumb.FileName);
            }
            if (fuFileUrl.HasFile)
            {
                var fuThumbName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", Utilities.GetPreByTime(DateTime.Now), fuFileUrl.FileName);
                SPFile file = Utilities.UploadFileToDocumentLibrary(CurrentWeb, fuFileUrl.PostedFile.InputStream, string.Format(CultureInfo.InvariantCulture,
                    "{0}/{1}/{2}", WebUrl, ListsName.InternalName.GalleryImagesList, fuFileUrl));
                CurrentItem[FieldsName.ResourceLibrary.InternalName.FileUrl] = file.Url;
                fileNames.Add(fuFileUrl.FileName);
            }



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

                if (CurrentMode.Equals(SPControlMode.Edit) || CurrentMode.Equals(SPControlMode.Display))
                {
                    ddlCategory.SelectedValue = Convert.ToString(CurrentItem[FieldsName.ResourceLibrary.InternalName.CategoryId]);
                }
                if (CurrentMode.Equals(SPControlMode.Display))
                {                    
                    ddlCategory.Visible = false;
                    lblCatDisplay.Visible = true;
                    lblCatDisplay.Text = Convert.ToString(CurrentItem[FieldsName.ResourceLibrary.InternalName.CategoryName]);
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