using System;
using System.Net.Http;
using System.Text;
using InterviewTask.Models;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;
using Application = Xamarin.Forms.Application;

namespace InterviewTask.ViewModels
{
	public class FormsConfigurePageViewModel:BaseViewModel
	{
        #region PrivateVariables
        private string selectedControl;
        private string englishText;
        private string arabicText;
        private bool isMandatory;
        #endregion

        public FormsConfigurePageViewModel()
		{
            SaveCommand = new Command(OnSave);
        }
        #region Properties
        public bool IsMandatory
        {
            get => isMandatory;
            set
            {
                isMandatory = value;
                OnPropertyChanged(nameof(IsMandatory));
            }
        }

        public string SelectedControl
        {
            get => selectedControl;
            set
            {
                selectedControl = value;
                OnPropertyChanged(nameof(SelectedControl));
            }
        }

        public string EnglishText
        {
            get => englishText;
            set
            {
                englishText = value;
                OnPropertyChanged(nameof(EnglishText));
            }
        }

        public string ArabicText
        {
            get => arabicText;
            set
            {
                arabicText = value;
                OnPropertyChanged(nameof(ArabicText));
            }
        }
        #endregion

        #region Method
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(SelectedControl)
                && !String.IsNullOrWhiteSpace(EnglishText)
                && !String.IsNullOrWhiteSpace(ArabicText);
        }

        private async void OnSave()
        {
            if(!ValidateSave())
            {
                StringBuilder message = new StringBuilder();
                if (String.IsNullOrWhiteSpace(SelectedControl))
                    message.Append("Control Type").AppendLine();
                if (String.IsNullOrWhiteSpace(EnglishText))
                    message.Append("Label English").AppendLine();
                if (String.IsNullOrWhiteSpace(ArabicText))
                    message.Append("Label Arabic");

                Application.Current.MainPage.DisplayAlert("Please fill following fields", message.ToString(), "OK");
            }
            else
            {
                FormsModel newItem = new FormsModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    ControlType = SelectedControl,
                    LabelEnglish = EnglishText,
                    LabelArabic = ArabicText,
                     IsCompulsory = IsMandatory
                };

                await FormsDataStore.AddItemAsync(newItem);

                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }
        }

        

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        #endregion

        #region Command
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        #endregion



    }
}

