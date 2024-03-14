using System;
using System.Collections.Generic;
using InterviewTask.Services;
using System.Globalization;
using InterviewTask.ViewModels;
using Xamarin.Forms;
using System.Threading;

namespace InterviewTask.Views
{	
	public partial class RichTextPage : ContentPage
	{
        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        public RichTextPage ()
		{
			InitializeComponent ();

            BindingContext = new RichTextPageViewModel();
        }

        void RichTextEditor_ContentRetrieved(System.Object sender, System.String e)
        {
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //await DependencyService.Get<IAudioRecorder>().Listen(CultureInfo.GetCultureInfo("en-us"),
            //       new Progress<string>(partialText =>
            //       {


            //       }), tokenSource.Token);
        }
    }
}

