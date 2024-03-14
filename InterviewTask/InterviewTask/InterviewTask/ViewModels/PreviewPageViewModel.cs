using System;
using InterviewTask.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json;
namespace InterviewTask.ViewModels
{
    [QueryProperty(nameof(NavParam), nameof(NavParam))]
    public class PreviewPageViewModel:BaseViewModel
    {
		public PreviewPageViewModel()
		{
		}

        private string navParam;

        public string NavParam
        {
            get {return navParam; }
            set
            {
                navParam = value;

                FormList = JsonConvert.DeserializeObject<ObservableCollection<FormsModel>>(navParam);

                OnLoadData();
            }
        }
        private ContentView finalView;

        public ContentView FinalView
        {
            get => finalView;
            set
            {
                finalView = value;
                OnPropertyChanged(nameof(FinalView));
            }
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

        public void OnLoadData()
        {
            if (FormList?.Count > 0)
            {
                StackLayout layout = new StackLayout() { VerticalOptions = LayoutOptions.FillAndExpand};
                for (int i = 0; i < FormList.Count ; i++)
                {
                    if (FormList[i].ControlType== "TextBox")
                    {
                        Label titleLabel = new Label() { HorizontalOptions = LayoutOptions.FillAndExpand };
                        titleLabel.Text = FormList[i].LabelEnglish;
                        layout.Children.Add(titleLabel);
                        Entry entry = new Entry();
                        layout.Children.Add(entry);
                    }
                    else if(FormList[i].ControlType == "DropDownList")
                    {
                        Label titleLabel = new Label() { HorizontalOptions = LayoutOptions.FillAndExpand};
                        titleLabel.Text = FormList[i].LabelEnglish;
                        layout.Children.Add(titleLabel);
                        Picker picker = new Picker();
                        picker.ItemsSource = new List<string>()
                        {
                            "Sample Data 1",
                            "Sample Data 2",
                            "Sample Data 3",
                        };

                        layout.Children.Add(picker);
                    }
                    else if (FormList[i].ControlType == "RadioButtonList")
                    {
                        StackLayout stackLayout = new StackLayout() { Orientation = StackOrientation.Vertical};

                        Label label = new Label
                        {
                            Text = "This is testing Radion Button?"
                        };

                        RadioButton sampleRadioButton1 = new RadioButton
                        {
                            Content = "Sample Data 1"
                        };

                        RadioButton sampleRadioButton2 = new RadioButton
                        {
                            Content = "ample Data 2"
                        };

                        RadioButton sampleRadioButton3 = new RadioButton
                        {
                            Content = "ample Data 3"
                        };

                        RadioButton sampleRadioButton4 = new RadioButton
                        {
                            Content = "ample Data 4"
                        };

                        stackLayout.Children.Add(label);
                        stackLayout.Children.Add(sampleRadioButton1);
                        stackLayout.Children.Add(sampleRadioButton2);
                        stackLayout.Children.Add(sampleRadioButton3);
                        stackLayout.Children.Add(sampleRadioButton4);

                        layout.Children.Add(stackLayout);
                    }
                    else if (FormList[i].ControlType == "TextArea")
                    {
                        Label titleLabel = new Label() { HorizontalOptions = LayoutOptions.FillAndExpand };
                        titleLabel.Text = FormList[i].LabelEnglish;
                        Editor entry = new Editor();
                        layout.Children.Add(entry);
                    }
                    else if (FormList[i].ControlType == "CheckBoxList")
                    {
                        Label titleLabel = new Label() { HorizontalOptions = LayoutOptions.FillAndExpand };
                        titleLabel.Text = FormList[i].LabelEnglish;
                        CheckBox checkBox = new CheckBox();
                        layout.Children.Add(checkBox);
                    }

                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    FinalView = new ContentView();
                    FinalView.Content = layout;
                    MessagingCenter.Send<string, ContentView>("UpdateUI", "UpdateUI", FinalView);

                });
                
            }

        }


    }
}

