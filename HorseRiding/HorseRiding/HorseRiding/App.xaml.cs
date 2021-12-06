using HorseRiding.Services;
using HorseRiding.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ChooseRolePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
