using System;
using System.Collections.Generic;
using InterviewTask.ViewModels;
using Xamarin.Forms;

namespace InterviewTask.Views
{	
	public partial class PreviewPage : ContentPage
	{
		PreviewPageViewModel viewModel;
		public PreviewPage ()
		{
            try
            {
                InitializeComponent();

                BindingContext = viewModel = new PreviewPageViewModel();

                MessagingCenter.Subscribe<string,ContentView>("UpdateUI", "UpdateUI",  (sender, arg) =>
                {
                    MyContentView.Content = arg;
                });

            }
            catch(Exception ex)
            {

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string,ContentView>("UpdateUI", "UpdateUI");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            
        }
    }
}

