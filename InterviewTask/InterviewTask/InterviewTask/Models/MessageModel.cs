using System;
namespace InterviewTask.Models
{
	public class MessageModel
	{
		public long Id { get; set; }

		public string UserName { get; set; }

		public string Message { get; set; }

		public bool IsOwnerMessage { get; set; }

		public bool IsSystemMessage { get; set; }

		public bool IsSystemQuery { get; set; }

		public QueryModel QueryResponse {get;set;}
    }
}
	
