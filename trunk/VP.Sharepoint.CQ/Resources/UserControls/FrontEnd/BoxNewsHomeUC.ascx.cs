using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using VP.Sharepoint.CQ.Common;
using System.Globalization;
using Constants = VP.Sharepoint.CQ.Common.Constants;
using FieldsName = VP.Sharepoint.CQ.Common.FieldsName;
using System.Data;
using System.Web.UI.WebControls;

namespace VP.Sharepoint.CQ.UserControls
{
    public partial class BoxNewsHomeUC : FrontEndUC
    {
        #region Form Events
        /// <summary>
        /// Load default value to control and other initialize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        protected int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            rptNewsSlide.ItemDataBound += new RepeaterItemEventHandler(rptNewsSlide_ItemDataBound);
            if (!Page.IsPostBack)
            {
                BindRepeater();
                imgSlide.Src = DocLibUrl + "/leftarrow.png";
                imgSlideR.Src = DocLibUrl + "/rightarrow.png";
            }
        }
        #endregion

        #region Bind repeater
        protected void BindRepeater()
        {
            DataTable dt= Utilities.GetNewsByStatus(CurrentWeb, Constants.NewsStatus.SlideNews);            
            rptNewsSlide.DataSource = dt;
            rptNewsSlide.DataBind();
        }
        #endregion

        protected void rptNewsSlide_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item)|| e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                HtmlAnchor aImg = (HtmlAnchor)e.Item.FindControl("aImg");
                HtmlAnchor aLink = (HtmlAnchor)e.Item.FindControl("aLink");
                HtmlImage imgNews = (HtmlImage)e.Item.FindControl("imgNews");
                Literal ltrDivHead = (Literal)e.Item.FindControl("ltrDivHead");
                Literal ltrDivBottom = (Literal)e.Item.FindControl("ltrDivBottom");

                ltrDivHead.Text = "<div class=\"panel\" id='panel_"+e.Item.ItemIndex+"' >";
                
                aImg.HRef = string.Format("../newsdetail.aspx?ID={0}&CatId={1}", drv["ID"], drv[FieldsName.NewsList.InternalName.NewsGroup]);
                aLink.HRef = aImg.HRef;
                aLink.Title = Convert.ToString(drv[FieldsName.NewsList.InternalName.Title]);
                aLink.InnerText = Convert.ToString(drv[FieldsName.NewsList.InternalName.Title]);
                var imgUrl = Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                if (imgUrl.Contains("http://"))
                {
                    imgNews.Src = imgUrl;
                }
                else
                {
                    imgNews.Src = WebUrl + "/" + Convert.ToString(drv[FieldsName.NewsList.InternalName.ImageThumb]);
                }
                ltrDivBottom.Text = "</div>";
                //
            }
        }
    }
}
