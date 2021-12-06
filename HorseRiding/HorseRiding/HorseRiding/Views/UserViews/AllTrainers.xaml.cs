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
    public partial class AllTrainers : ContentPage
    {
        public AllTrainers()
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
            lvTrainers.ItemsSource = await APIManager.GetData<List<Trainer>>("Trainers");
            refreshView.IsRefreshing = false;
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            LoadTrainers();
        }
    }
}