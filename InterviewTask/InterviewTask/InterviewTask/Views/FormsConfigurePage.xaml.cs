using System;
using System.Collections.Generic;
using InterviewTask.ViewModels;
using Xamarin.Forms;

namespace InterviewTask.Views
{	
	public partial class FormsConfigurePage : ContentPage
	{	
		public FormsConfigurePage ()
		{
			InitializeComponent ();

			BindingContext = new FormsConfigurePageViewModel();
        }
	}
}

