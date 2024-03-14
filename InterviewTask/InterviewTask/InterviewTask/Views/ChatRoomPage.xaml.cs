using System;
using System.Collections.Generic;
using InterviewTask.ViewModels;
using Xamarin.Forms;

namespace InterviewTask.Views
{	
	public partial class ChatRoomPage : ContentPage
	{
		ChatRoomPageViewModel viewModel;
		public ChatRoomPage ()
		{
			InitializeComponent ();

            viewModel = new ChatRoomPageViewModel();
			BindingContext = viewModel;

		}

    }
}

