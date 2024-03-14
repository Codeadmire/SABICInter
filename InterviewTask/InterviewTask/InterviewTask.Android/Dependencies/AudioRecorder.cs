using System;
using Android.Media;
using InterviewTask.Services;
using System.Threading.Tasks;
using InterviewTask.Droid.Dependencies;
using Xamarin.Forms;
using Android.Speech;
using Android.OS;
using Android.Runtime;
using System.Linq;
using System.Globalization;
using System.Threading;
using Xamarin.Essentials;
using Android.Content;

[assembly: Dependency(typeof(AudioRecorder))]

namespace InterviewTask.Droid.Dependencies
{
    public class AudioRecorder : IAudioRecorder
    {
        MediaRecorder recorder;
        string filePath;

        private SpeechRecognitionListener listener;
        private SpeechRecognizer speechRecognizer;
        public AudioRecorder()
        {
           
        }
        public AudioRecorder(string path)
        {
            filePath = path;
        }

        public async Task StartAudioRecording()
        {
            if (recorder == null)
                recorder = new MediaRecorder();

            recorder.SetAudioSource(AudioSource.Mic);
            recorder.SetOutputFormat(OutputFormat.Default);
            recorder.SetAudioEncoder(AudioEncoder.Default);
            recorder.SetOutputFile(filePath);

            recorder.Prepare();
            recorder.Start();
        }

        public async Task StopAudioRecording()
        {
            if (recorder != null)
            {
                recorder.Stop();
                recorder.Reset();
                recorder.Release();
                recorder = null;
            }
        }

        public async Task<string> Listen(CultureInfo culture,
            IProgress<string> recognitionResult,
            CancellationToken cancellationToken)
        {
            var taskResult = new TaskCompletionSource<string>();
            listener = new SpeechRecognitionListener
            {
                Error = ex => taskResult.TrySetException(new Exception("Failure in speech engine - " + ex)),
                PartialResults = async sentence =>
                {
                  await  StartAudioRecording();
                    //recognitionResult?.Report(sentence);
                },
                Results = sentence => taskResult.TrySetResult(sentence)
            };

            speechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(Android.App.Application.Context);

            if (speechRecognizer is null)
            {
                throw new ArgumentException("Speech recognizer is not available");
            }

            speechRecognizer.SetRecognitionListener(listener);
            speechRecognizer.StartListening(CreateSpeechIntent(culture));

            await using (cancellationToken.Register(async () =>
            {
                StopRecording();
               await StopAudioRecording();
                taskResult.TrySetCanceled();
            }))
            {
                return await taskResult.Task;
            }
        }

        private void StopRecording()
        {
            speechRecognizer?.StopListening();
            speechRecognizer?.Destroy();
        }

        private Intent CreateSpeechIntent(CultureInfo culture)
        {
            var intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            intent.PutExtra(RecognizerIntent.ExtraLanguagePreference, Java.Util.Locale.Default);
            var javaLocale = Java.Util.Locale.ForLanguageTag(culture.Name);
            intent.PutExtra(RecognizerIntent.ExtraLanguage, javaLocale);
            intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            intent.PutExtra(RecognizerIntent.ExtraCallingPackage, Android.App.Application.Context.PackageName);
            //intent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            //intent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            //intent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            //intent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            intent.PutExtra(RecognizerIntent.ExtraPartialResults, true);

            return intent;
        }

        public async Task<bool> RequestPermissions()
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();
            var isAvailable = SpeechRecognizer.IsRecognitionAvailable(Android.App.Application.Context);
            return status == PermissionStatus.Granted && isAvailable;

        }

    }

    public class SpeechRecognitionListener : Java.Lang.Object, IRecognitionListener
    {
        public Action<SpeechRecognizerError>? Error { get; set; }
        public Action<string>? PartialResults { get; set; }
        public Action<string>? Results { get; set; }
        public void OnBeginningOfSpeech()
        {

        }

        public void OnBufferReceived(byte[]? buffer)
        {
        }

        public void OnEndOfSpeech()
        {
        }

        public void OnError([GeneratedEnum] SpeechRecognizerError error)
        {
            Error?.Invoke(error);
        }

        public void OnEvent(int eventType, Bundle? @params)
        {
        }

        public void OnPartialResults(Bundle? partialResults)
        {
            SendResults(partialResults, PartialResults);
        }

        public void OnReadyForSpeech(Bundle? @params)
        {
        }

        public void OnResults(Bundle? results)
        {
            SendResults(results, Results);
        }

        public void OnRmsChanged(float rmsdB)
        {
        }

        void SendResults(Bundle? bundle, Action<string>? action)
        {
            var matches = bundle?.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
            if (matches == null || matches.Count == 0)
            {
                return;
            }

            action?.Invoke(matches.First());
        }
    }
}