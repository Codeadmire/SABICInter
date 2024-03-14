using System;
using InterviewTask.Models;
using Xamarin.Forms;

namespace InterviewTask.DataTemplates
{
	public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OwnerTemplate { get; set; }

        public DataTemplate OthersTemplate { get; set; }

        public DataTemplate QueryResponseTemplate { get; set; }

        public DataTemplate SystemTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (((MessageModel)item).IsOwnerMessage && !((MessageModel)item).IsSystemQuery && !((MessageModel)item).IsSystemMessage)
            {
                return OwnerTemplate ;
            }
            else if (!((MessageModel)item).IsOwnerMessage && ((MessageModel)item).IsSystemQuery && !((MessageModel)item).IsSystemMessage)
            {
                return QueryResponseTemplate;
            }
            else if (!((MessageModel)item).IsOwnerMessage && !((MessageModel)item).IsSystemQuery && ((MessageModel)item).IsSystemMessage)
            {
                return SystemTemplate;
            }
            else
            {
                return OthersTemplate;
            }
        }
    }
}

