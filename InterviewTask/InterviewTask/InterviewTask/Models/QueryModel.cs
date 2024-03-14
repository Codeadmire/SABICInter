using System;
using System.Collections.Generic;

namespace InterviewTask.Models
{
	public class QueryModel
	{
        public string Title { get; set; }

        public string Description { get; set; }

        public List<AttachmentModel> Attachment { get; set; }

        public List<AttachmentModel> Actions { get; set; }
    }
}

