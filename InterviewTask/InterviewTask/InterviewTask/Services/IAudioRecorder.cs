using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewTask.Services
{
	public interface IAudioRecorder
	{
        Task StartAudioRecording();
        Task StopAudioRecording();

        Task<bool> RequestPermissions();

        Task<string> Listen(CultureInfo culture,
            IProgress<string> recognitionResult,
            CancellationToken cancellationToken);
    }
}

