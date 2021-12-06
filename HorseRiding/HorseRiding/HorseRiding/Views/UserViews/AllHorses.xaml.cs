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
    public partial class AllHorses : ContentPage
    {
        public AllHorses()
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
            lvHorses.ItemsSource = await APIManager.GetData<List<Horse>>("Horses");
            refreshView.IsRefreshing = false;
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            LoadHorses();
        }
    }
}