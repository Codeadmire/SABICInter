using System;
using System.Collections.Generic;
using InterviewTask.ViewModels;
using Xamarin.Forms;

namespace InterviewTask.Views
{	
	public partial class FormsPage : ContentPage
	{
        FormsPageViewModel _viewModel;
        public FormsPage ()
		{
			InitializeComponent ();

            BindingContext = _viewModel= new FormsPageViewModel();
            
        }

        async void ScrollView_Scrolled(System.Object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
           await scrollviewContent.ScrollToAsync(e.ScrollX, e.ScrollY,false);
        }

        async void scrollviewContent_Scrolled(System.Object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
            await ScrollHeader.ScrollToAsync(e.ScrollX, ScrollHeader.ScrollY, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.OnAppearing();

        }
    }
}

