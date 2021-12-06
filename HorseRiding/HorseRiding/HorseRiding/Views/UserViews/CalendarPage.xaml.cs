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

namespace HorseRiding.Views.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        public EventCollection trainings;
        public CalendarPage()
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
            var userTrainings = await APIManager.GetData<List<UserTraining>>($"UserTrainings?userId={UserShell.LoggedUser.Id}");
            trainings = new EventCollection();
            foreach (var userTraining in userTrainings.GroupBy(g => g.TrainingDate))
            {
                trainings.Add(userTraining.Key, userTraining.ToList());
            }
            trainingsCalendar.Events = trainings;
        }
        private async void bAddTraining_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TrainingPage));
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}