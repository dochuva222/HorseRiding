using HorseRiding.Models;
using HorseRiding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding.Views.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbacksPage : ContentPage
    {
        Feedback contextFeedback;
        public FeedbacksPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private async void LoadMyFeedback()
        {
            contextFeedback = await APIManager.GetData<Feedback>($"Feedbacks?userId={UserShell.LoggedUser.Id}");
            if (contextFeedback == null)
            {
                myFeedback.IsVisible = false;
                bAdd.IsVisible = true;
                return;
            }
            BindingContext = contextFeedback;
            myFeedback.IsVisible = true;
            bAdd.IsVisible = false;
            myRating.Rating = contextFeedback.Rating;
        }

        private async void Refresh()
        {
            refreshView.IsRefreshing = true;
            var feedbacks = await APIManager.GetData<List<Feedback>>("Feedbacks");
            lvFeedbacks.ItemsSource = feedbacks.Where(f => f.UserId != UserShell.LoggedUser.Id).ToList();
            LoadMyFeedback();
            refreshView.IsRefreshing = false;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FeedbackPage));
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }

        private async void bEdit_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FeedbackPage));
        }

        private void bDelete_Clicked(object sender, EventArgs e)
        {
            APIManager.DeleteData($"Feedbacks/{contextFeedback.Id}");
            Refresh();
        }

        private async void bAdd_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FeedbackPage));
        }
    }
}