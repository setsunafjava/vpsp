using Microsoft.SharePoint;
using VP.Sharepoint.CQ.Common;
using VP.Sharepoint.CQ.Core.Helpers;

namespace VP.Sharepoint.CQ
{
    class CreateNewsListStructre
    {
        public static void CreateListStructure(SPWeb web)
        {
            
            var helper = new ListHelper(web)
                        {
                            Title = ListsName.DisplayName.NewsList,
                            Name = ListsName.InternalName.NewsList,
                            OnQuickLaunch = false, EnableAttachments = true
                        };

            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsList.InternalName.NewsGroup, FieldsName.NewsList.DisplayName.NewsGroup));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsList.InternalName.NewsGroupName, FieldsName.NewsList.DisplayName.NewsGroupName));

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsList.InternalName.Description, FieldsName.NewsList.DisplayName.Description) 
            { 
                RichText = true,
                RichTextMode = SPRichTextMode.FullHtml
            });           

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsList.InternalName.Content, FieldsName.NewsList.DisplayName.Content)
            {
                RichText = true,
                RichTextMode = SPRichTextMode.FullHtml,
                NumberOfLines = 20
            });
            helper.AddField(new UserFieldCreator(FieldsName.NewsList.InternalName.Poster, FieldsName.NewsList.DisplayName.Poster) 
                { 
                    SelectionMode = SPFieldUserSelectionMode.PeopleOnly, 
                    AllowMultipleValues = false }
                );
            helper.AddField(new MultipleChoiceFieldCreator(FieldsName.NewsList.InternalName.Status, FieldsName.NewsList.DisplayName.Status) 
            { 
                Choices = { Constants.NewsStatus.HomeNews, Constants.NewsStatus.HotNews, Constants.NewsStatus.SlideNews,Constants.NewsStatus.ShouldKnowNews }, 
                DefaultValue = Constants.NewsStatus.HomeNews                
            });
            helper.AddField(new DateTimeFieldCreator(FieldsName.NewsList.InternalName.PostedDate, FieldsName.NewsList.DisplayName.PostedDate)
                {
                     DefaultValue = "[ToDay]",
                     DisplayFormat = SPDateTimeFieldFormatType.DateTime
                }
                );

            // Thumbnai image
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsList.InternalName.ImageThumb, FieldsName.NewsList.DisplayName.ImageThumb));
            // Small thumbnai image
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsList.InternalName.ImageSmallThumb, FieldsName.NewsList.DisplayName.ImageSmallThumb));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsList.InternalName.ImageHot, FieldsName.NewsList.DisplayName.ImageHot));

            helper.AddField(new UrlFieldCreator(FieldsName.NewsList.InternalName.ImageDsp, FieldsName.NewsList.DisplayName.ImageDsp) { DisplayFormat = SPUrlFieldFormatType.Image });
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsList.InternalName.NewsCount, FieldsName.NewsList.DisplayName.NewsCount));
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsList.InternalName.NewsUrl, FieldsName.NewsList.DisplayName.NewsUrl));

            var list = helper.Apply();
            
            SPField fieldTitle = list.Fields.GetField(FieldsName.NewsList.InternalName.Title);
            if (fieldTitle != null)
            {
                fieldTitle.Title = FieldsName.NewsList.DisplayName.Title;
                fieldTitle.Update();
            }

            //Set menu link
            Utilities.SetMenuLink(list, FieldsName.NewsList.InternalName.Status);

            //Add event receiver
            Utilities.CreateEventReceivers(list, "VP.Sharepoint.CQ.EventReceivers.NewsEventReceiver", SPEventReceiverType.ItemAdded);
            Utilities.CreateEventReceivers(list, "VP.Sharepoint.CQ.EventReceivers.NewsEventReceiver", SPEventReceiverType.ItemUpdated);

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/NewsList.ascx");

            //Create view
            var viewFields=new []{
                    FieldsName.NewsList.InternalName.Title                   
                };
            Utilities.AddStandardView(list, "AllNews", viewFields, "'<GroupBy Collapse='TRUE'><FieldRef Name='NewsGroupName' /></GroupBy>'", 100, true);
        }
    }
}
