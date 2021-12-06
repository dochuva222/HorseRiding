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
    public partial class CoursesPage : ContentPage
    {
        public CoursesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadTypes();
        }

        private async void LoadTypes()
        {
            refreshView.IsRefreshing = true;
            lvHorses.ItemsSource = await APIManager.GetData<List<TrainingType>>("TrainingTypes");
            refreshView.IsRefreshing = false;
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            LoadTypes();
        }
    }
}