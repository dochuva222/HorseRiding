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
    public partial class AdminHorsesPage : ContentPage
    {
        public AdminHorsesPage()
        {
            InitializeComponent();
        }

        private async void Refresh()
        {
            refreshView.IsRefreshing = true;
            var horses = await APIManager.GetData<List<Horse>>("Horses");
            lvHorses.ItemsSource = horses;
            refreshView.IsRefreshing = false;

        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private async void bEdit_Clicked(object sender, EventArgs e)
        {
            var selectedHorse = (sender as Button).BindingContext as Horse;
            await Shell.Current.GoToAsync($"{nameof(AdminHorsePage)}?SentHorseId={selectedHorse.Id}");
        }

        private void bDelete_Clicked(object sender, EventArgs e)
        {
            var selectedHorse = (sender as Button).BindingContext as Horse;
            APIManager.DeleteData($"DeleteHorse/{selectedHorse.Id}");
            Refresh();
        }

        private async void bAddHorse_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(AdminHorsePage)}?SentHorseId=0");
        }
    }
}