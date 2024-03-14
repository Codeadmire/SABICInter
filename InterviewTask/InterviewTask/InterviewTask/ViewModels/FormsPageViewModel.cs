using System;
using InterviewTask.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using InterviewTask.Views;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

namespace InterviewTask.ViewModels
{
	public class FormsPageViewModel:BaseViewModel
	{
        
        public FormsPageViewModel()
		{
            DeleteFormCommand = new Command<FormsModel>(DeleteForm);
            AddItemCommand = new Command(OnAddItem);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            PreviewCommand = new Command(async () => await ExecutePreview() );
            FormList = new ObservableCollection<FormsModel>();
        }

        #region Properties
        bool isPreviewVisible = false;
        public bool IsPreviewVisible
        {
            get { return isPreviewVisible; }
            set { SetProperty(ref isPreviewVisible, value); }
        }
        private ObservableCollection<FormsModel> formList;

        public ObservableCollection<FormsModel> FormList
        {
            get => formList;
            set
            {
                formList = value;
               
                OnPropertyChanged(nameof(FormList));
            }
        }

        #endregion

        #region Method

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                FormList.Clear();
                var items = await FormsDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    FormList.Add(item);
                }


                IsPreviewVisible = FormList?.Count > 0;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddItem()
        {
            await Shell.Current.GoToAsync(nameof(FormsConfigurePage));
        }

        private async void DeleteForm(FormsModel model)
        {
            //if(model is FormsModel item)
            await FormsDataStore.DeleteItemAsync(model.Id);
            FormList.Remove(model);

            IsPreviewVisible = FormList?.Count > 0;
        }

        private async Task ExecutePreview()
        {
            if (FormList?.Count > 0)
            {
                string json = JsonConvert.SerializeObject(formList);

                
                await Shell.Current.GoToAsync($"{nameof(PreviewPage)}?{nameof(PreviewPageViewModel.NavParam)}={json}");
            }

        }

        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedItem = null;
        }

        #endregion

        #region Command
        public ICommand DeleteFormCommand { get; }
        public Command LoadItemsCommand { get; }

        public Command AddItemCommand { get; }
        public Command PreviewCommand { get; }
        #endregion
    }
}

