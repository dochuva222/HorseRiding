using HorseRiding.Models;
using HorseRiding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminFeedbacksPage : ContentPage
    {
        public AdminFeedbacksPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private async void Refresh()
        {
            refreshView.IsRefreshing = true;
            lvFeedbacks.ItemsSource = await APIManager.GetData<List<Feedback>>("Feedbacks");
            refreshView.IsRefreshing = false;
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }

        private void bDelete_Clicked(object sender, EventArgs e)
        {
            var selectedFeedback = (sender as Button).BindingContext as Feedback;
            APIManager.DeleteData($"Feedbacks/{selectedFeedback.Id}");
            Refresh();
        }
    }
}