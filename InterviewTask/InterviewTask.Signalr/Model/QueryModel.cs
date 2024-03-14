using System;
namespace InterviewTask.Signalr.Model
{
	public class QueryModel
	{
		public string Title { get; set; }

        public string Description { get; set; }

		public List<AttachmentModel> Attachment { get; set; }

        public List<AttachmentModel> Actions { get; set; }
    }
}

