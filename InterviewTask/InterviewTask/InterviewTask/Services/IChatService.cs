using System;
using System.Threading.Tasks;

namespace InterviewTask.Services
{
	public interface IChatService
	{
        Task Connect();
        Task DisConnect();
        Task SendMessage(string userId, string message);
        void ReceiveMessage(Action<string, string> GetMessageAndUser);
    }
}

