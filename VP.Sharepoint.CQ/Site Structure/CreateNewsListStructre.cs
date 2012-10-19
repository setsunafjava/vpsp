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

            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsList.InternalName.Description, FieldsName.NewsList.DisplayName.Description) 
            { 
                RichText = true,
                RichTextMode = SPRichTextMode.FullHtml
            });
            helper.AddField(new MultipleLinesTextFieldCreator(FieldsName.NewsList.InternalName.Content, FieldsName.NewsList.DisplayName.Content)
            {
                RichText = true,
                RichTextMode = SPRichTextMode.FullHtml
            });
            helper.AddField(new UserFieldCreator(FieldsName.NewsList.InternalName.Poster, FieldsName.NewsList.DisplayName.Poster) 
                { 
                    SelectionMode = SPFieldUserSelectionMode.PeopleOnly, 
                    AllowMultipleValues = false }
                );
            helper.AddField(new SingleLineTextFieldCreator(FieldsName.NewsList.InternalName.Status, FieldsName.NewsList.DisplayName.Status));
            helper.AddField(new DateTimeFieldCreator(FieldsName.NewsList.InternalName.PostedDate, FieldsName.NewsList.DisplayName.PostedDate)
                {
                     DefaultValue = "[ToDay]",
                     DisplayFormat = SPDateTimeFieldFormatType.DateTime
                }
                );

            var list = helper.Apply();

            //Set menu link
            Utilities.SetMenuLink(list, FieldsName.NewsList.InternalName.Status);

            //Add event receiver
            Utilities.CreateEventReceivers(list, "VP.Sharepoint.CQ.EventReceivers.NewsEventReceiver", SPEventReceiverType.ItemAdded);
            Utilities.CreateEventReceivers(list, "VP.Sharepoint.CQ.EventReceivers.NewsEventReceiver", SPEventReceiverType.ItemUpdated);

            //Add custom usercontrol to form
            Utilities.AddForms(web, list, "../../UserControls/NewsList.ascx");
        }
    }
}
