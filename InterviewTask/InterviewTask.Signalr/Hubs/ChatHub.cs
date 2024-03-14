using System;
using System.Collections.Generic;
using System.Text;
using InterviewTask.Signalr.Model;
using Microsoft.AspNetCore.SignalR;

namespace InterviewTask.Signalr.Hubs
{
	public class ChatHub:Hub
	{
        public async Task JoinChat(string user)
        {
            await Clients.All.SendAsync("JoinChat", user);
        }

        public async Task LeaveChat(string user)
        {
            await Clients.All.SendAsync("LeaveChat", user);
        }
        public async Task SendMessage(string userId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userId, message);
        }

        public async Task QueryMessage(string userId, string message)
        {
            if (message.ToLower().Contains("web app performance"))
            {
                QueryModel response = new QueryModel();
                response.Title = "There are serveral ways to optimize the performance of web application. Here are some suggestions:";
                response.Description = new StringBuilder().
                    Append(QueryReplyConstant.Performanceoptimization1).AppendLine()
                    .Append(QueryReplyConstant.Performanceoptimization2).AppendLine()
                    .Append(QueryReplyConstant.Performanceoptimization3).ToString();
                response.Attachment = new List<AttachmentModel> { new AttachmentModel() { Name= "Attachment1" }, new AttachmentModel() { Name = "Attachment2" }, new AttachmentModel() { Name = "Attachment2" } };
                response.Actions = new List<AttachmentModel> { new AttachmentModel() { Name = "Copy" }, new AttachmentModel() { Name = "Share" } };
                await Clients.All.SendAsync("QueryResponse", userId, response);
            }
            else
            {
                Random rnd = new Random();
                QueryModel response = new QueryModel();
                response.Title = QueryReplyConstant.TitleList[rnd.Next(QueryReplyConstant.TitleList.Count)];
                response.Description = new StringBuilder().
                    Append(QueryReplyConstant.ResponseList[rnd.Next(QueryReplyConstant.TitleList.Count)]).AppendLine()
                    .Append(QueryReplyConstant.ResponseList[rnd.Next(QueryReplyConstant.TitleList.Count)]).AppendLine()
                    .Append(QueryReplyConstant.TitleList[rnd.Next(QueryReplyConstant.TitleList.Count)]).ToString();
                await Clients.All.SendAsync("QueryResponse", userId, response);
            }
        }
    }
}

