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
    [QueryProperty(nameof(SentPage), nameof(SentPage))]
    [QueryProperty(nameof(TrainingTypeId), nameof(TrainingTypeId))]
    public partial class HorsesPage : ContentPage
    {
        public string SentPage { get; set; }
        public string TrainingTypeId { get; set; }
        public HorsesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadHorses();
        }

        private async void LoadHorses()
        {
            refreshView.IsRefreshing = true;
            lvHorses.ItemsSource = await APIManager.GetData<List<Horse>>($"Horses?trainingTypeId={TrainingTypeId}");
            refreshView.IsRefreshing = false;
        }

        private async void lvHorses_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (SentPage == "Rent")
                MessagingCenter.Send(new RentPage(), "HorseChange", lvHorses.SelectedItem as Horse);
            if (SentPage == "Course")
                MessagingCenter.Send(new TrainingPage(), "HorseChange", lvHorses.SelectedItem as Horse);
            await Shell.Current.GoToAsync("..");
        }

        private async void bBack_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            LoadHorses();
        }
    }
}