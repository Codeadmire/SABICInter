using System;
using System.Globalization;
using System.Threading;
using InterviewTask.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterviewTask.ViewModels
{
	public class AudioPageViewModel : BaseViewModel
	{
        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        //IAudioRecorder audioRecorder;
        public AudioPageViewModel()
		{
			
		}

    }
}

