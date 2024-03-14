using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterviewTask.CustomControls
{
	public class RichTextEditor : ContentView
    {
        WebView webView;
        Button getContentButton;


        public static readonly BindableProperty ContentRetrievedCommandProperty =
          BindableProperty.Create(nameof(ContentRetrievedCommand), typeof(Command<string>), typeof(RichTextEditor));


        public Command<string> ContentRetrievedCommand
        {
            get { return (Command<string>)GetValue(ContentRetrievedCommandProperty); }
            set { SetValue(ContentRetrievedCommandProperty, value); }
        }

        public RichTextEditor()
        {
            InitializeUI();
        }

        async void InitializeUI()
        {
            // WebView for displaying rich text editor
            webView = new WebView() { };
          
            var htmlContent = await GetHtmlContent("richtext_file.html");

            webView.Source =  new HtmlWebViewSource
            {
                Html = htmlContent
            };

            // Button for retrieving content
            getContentButton = new Button { Text = "Get Content" };
            getContentButton.Clicked += OnGetContentButtonClicked;

            // Add WebView and Button to content layout
            var grid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection()
                {
                     new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                    new RowDefinition(){Height = GridLength.Auto},
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Grid.SetRow(webView, 0);
            Grid.SetRow(getContentButton, 1);
            grid.Children.Add(webView);
            grid.Children.Add(getContentButton);
            this.Content = grid;
        }
        private async Task<string> GetHtmlContent(string fileName)
        {
            var assembly = typeof(MainPage).Assembly;
            var stream = assembly.GetManifestResourceStream($"InterviewTask.Assets.{fileName}");

            if (stream != null)
            {
                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }

            return string.Empty;
        }
        async void OnGetContentButtonClicked(object sender, EventArgs e)
        {
            string htmlContent = await webView.EvaluateJavaScriptAsync("getContent();");
            ContentRetrievedCommand?.Execute(htmlContent);
        }
    }
}