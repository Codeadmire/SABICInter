using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterviewTask.ViewModels
{
	public class RichTextPageViewModel : BaseViewModel
    {
		public RichTextPageViewModel()
		{
            ContentRetrievedCommand = new Command<string>(OnContentRetrieved);


        }

        private void OnContentRetrieved(string htmlContent)
        {
            Application.Current.MainPage.DisplayAlert("Rich Text Editor Content", htmlContent, "OK");

        }

        public ICommand ContentRetrievedCommand { get; private set; }
    }
}

