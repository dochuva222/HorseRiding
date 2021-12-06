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
    [QueryProperty(nameof(TrainingTypeId),nameof(TrainingTypeId))]
    public partial class TrainersPage : ContentPage
    {
        public string TrainingTypeId { get; set; }
        public TrainersPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadTrainers();
        }

        private async void LoadTrainers()
        {
            refreshView.IsRefreshing = true;
            lvTrainers.ItemsSource = await APIManager.GetData<List<Trainer>>($"Trainers?trainingTypeId={TrainingTypeId}");
            refreshView.IsRefreshing = false;
        }

        private async void lvTrainers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagingCenter.Send(new TrainingPage(), "TrainerChange", lvTrainers.SelectedItem as Trainer);
            await Shell.Current.GoToAsync("..");
        }

        private async void bBack_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            LoadTrainers();   
        }
    }
}