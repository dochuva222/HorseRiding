using HorseRiding.Models;
using HorseRiding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace HorseRiding.Views.TrainerViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainerCalendarPage : ContentPage
    {
        public EventCollection trainings;

        public TrainerCalendarPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private void Refresh()
        {
            refreshView.IsRefreshing = true;
            LoadTrainings();
            refreshView.IsRefreshing = false;
        }

        private async void LoadTrainings()
        {
            var userTrainings = await APIManager.GetData<List<UserTraining>>("UserTrainings");
            trainings = new EventCollection();
            foreach (var userTraining in userTrainings.Where(u => u.TrainerId == TrainerProfile.LoggedTrainer.Id).GroupBy(g => g.TrainingDate))
            {
                trainings.Add(userTraining.Key, userTraining.ToList());
            }
            trainingsCalendar.Events = trainings;
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}