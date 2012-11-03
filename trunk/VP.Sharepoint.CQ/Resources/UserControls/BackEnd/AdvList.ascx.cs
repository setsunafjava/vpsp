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
    public partial class AdvList : BackEndUC, IValidator
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
            if (fuFile.HasFile)
            {
                var fuFileName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", Utilities.GetPreByTime(DateTime.Now), fuFile.FileName);
                SPFile file = Utilities.UploadFileToDocumentLibrary(CurrentWeb, fuFile.PostedFile.InputStream, string.Format(CultureInfo.InvariantCulture,
                    "{0}/{1}/{2}", CurrentWeb.Url, ListsName.InternalName.AdvFileList, fuFileName));
                CurrentItem[FieldsName.AdvList.InternalName.AdvFile] = file.Url;
            }
            if (CurrentMode.Equals(Constants.NewForm))
            {
                CurrentItem[FieldsName.AdvList.InternalName.AdvID] = new Guid();
            }
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
