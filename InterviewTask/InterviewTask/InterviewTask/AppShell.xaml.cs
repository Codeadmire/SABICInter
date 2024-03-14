using System;
using System.Collections.Generic;
using InterviewTask.ViewModels;
using InterviewTask.Views;
using Xamarin.Forms;

namespace InterviewTask
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(FormsPage), typeof(FormsPage));
            Routing.RegisterRoute(nameof(FormsConfigurePage), typeof(FormsConfigurePage));
            Routing.RegisterRoute(nameof(ChatRoomPage), typeof(ChatRoomPage));
            Routing.RegisterRoute(nameof(PreviewPage), typeof(PreviewPage));
        }

    }
}

