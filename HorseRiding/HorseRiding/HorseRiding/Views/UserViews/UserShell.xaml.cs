using HorseRiding.Models;
using HorseRiding.Views;
using HorseRiding.Views.UserViews;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HorseRiding
{
    public partial class UserShell : Xamarin.Forms.Shell
    {
        public static User LoggedUser;
        public UserShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RentPage), typeof(RentPage));
            Routing.RegisterRoute(nameof(FeedbackPage), typeof(FeedbackPage));
            Routing.RegisterRoute(nameof(UserLoginPage), typeof(UserLoginPage));
            Routing.RegisterRoute(nameof(TrainingPage), typeof(TrainingPage));
            Routing.RegisterRoute(nameof(HorsesPage), typeof(HorsesPage));
            Routing.RegisterRoute(nameof(TrainersPage), typeof(TrainersPage));
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            Application.Current.MainPage = new NavigationPage(new ChooseRolePage());
        }

        private async void miYoutube_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://www.youtube.com/c/%D0%A8%D0%BA%D0%BE%D0%BB%D0%B0%D0%92%D0%B5%D1%80%D1%85%D0%BE%D0%B2%D0%BE%D0%B9%D0%95%D0%B7%D0%B4%D1%8B%D0%9E%D0%BB%D1%8C%D0%B3%D0%B8%D0%AF%D0%B1%D0%BB%D0%BE%D0%BA%D0%BE%D0%B2%D0%BE%D0%B9/featured");
        }
    }
}
