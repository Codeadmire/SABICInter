using System.ComponentModel;
using Xamarin.Forms;
using InterviewTask.ViewModels;

namespace InterviewTask.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
