using System;
using System.Threading.Tasks;
using InterviewTask.Models;
using InterviewTask.Services;
using InterviewTask.Views;
using Xamarin.Forms;

namespace InterviewTask.ViewModels
{
	public class ChatPageViewModel: BaseViewModel
	{
        private string userName;

        public ChatPageViewModel()
		{
            JoinCommand = new Command(async()=>await JoinCommandExecute());

        }

        private async Task JoinCommandExecute()
        {
            await Shell.Current.GoToAsync($"{nameof(ChatRoomPage)}?{nameof(UserName)}={UserName}");

        }

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public Command JoinCommand { get; }
    }
}

