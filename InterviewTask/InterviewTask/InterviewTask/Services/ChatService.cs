using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace InterviewTask.Services
{
	public class ChatService : IChatService
    {
		private readonly HubConnection hubConnection;
		public ChatService()
		{
			hubConnection = new HubConnectionBuilder().WithUrl("http://127.0.0.1:5052/chathub").Build();
		}

		public async Task Connect()
		{
			await hubConnection.StartAsync();
		}
        public async Task DisConnect()
        {
            await hubConnection.StopAsync();
        }
		public async Task SendMessage(string userId,string message)
		{
            await hubConnection.SendCoreAsync("SendMessage",new object[] { userId, message });
        }

        public void ReceiveMessage(Action<string, string> GetMessageAndUser)
        {

            hubConnection.On("ReceiveMessage", GetMessageAndUser);

        }
    }
}

