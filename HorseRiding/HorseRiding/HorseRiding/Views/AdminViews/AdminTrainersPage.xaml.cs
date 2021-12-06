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
    public partial class AdminTrainersPage : ContentPage
    {
        public AdminTrainersPage()
        {
            InitializeComponent();
        }

        private async void Refresh()
        {
            refreshView.IsRefreshing = true;
            lvTrainers.ItemsSource = await APIManager.GetData<List<Trainer>>("Trainers");
            refreshView.IsRefreshing = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }

        private async void bEdit_Clicked(object sender, EventArgs e)
        {
            var selectedTrainer = (sender as Button).BindingContext as Trainer;
            await Shell.Current.GoToAsync($"{nameof(AdminTrainerPage)}?SentTrainerId={selectedTrainer.Id}");
        }

        private void bDelete_Clicked(object sender, EventArgs e)
        {
            var selectedTrainer = (sender as Button).BindingContext as Trainer;
            APIManager.DeleteData($"DeleteTrainer/{selectedTrainer.Id}");
            Refresh();
        }

        private async void bAddTrainer_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(AdminTrainerPage)}?SentTrainerId=0");
        }
    }
}