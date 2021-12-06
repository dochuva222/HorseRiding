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
    public partial class AdminTrainingsPage : ContentPage
    {
        public AdminTrainingsPage()
        {
            InitializeComponent();
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }

        private async void Refresh()
        {
            refreshView.IsRefreshing = true;
            lvTrainings.ItemsSource = await APIManager.GetData<List<UserTraining>>("ActualTrainings");
            refreshView.IsRefreshing = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private async void bDelete_Clicked(object sender, EventArgs e)
        {
            var selectedTraining = (sender as Button).BindingContext as UserTraining;
            var messageContent = $"Тренировка {selectedTraining.TrainingDate.ToShortDateString()} {selectedTraining.TrainingTime} отменена";
            await Shell.Current.GoToAsync($"{nameof(SendMessagePage)}?SentUserId={selectedTraining.UserId}&MessageContent={messageContent}&TrainingId={selectedTraining.Id}");
        }
    }
}