using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InterviewTask.Services;
using InterviewTask.Views;
using Xamarin.Essentials;
using System.Globalization;
using System.Threading;

namespace InterviewTask
{
    public partial class App : Application
    {
        public static string AzureBackendUrl = "https://azure-signalr-explore.azurewebsites.net";
        // DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;
        public App ()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockDataForms>();
            DependencyService.Register<IChatService,ChatService>();
            //DependencyService.Register<IAudioRecorder, AudioRecorder>();
            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {


            //var status = await Permissions.RequestAsync<Permissions.Microphone>();
            //if (status != PermissionStatus.Granted)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Permission Denied", "Microphone permission is required to record audio.", "OK");
            //    return;
            //}

          

        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}
