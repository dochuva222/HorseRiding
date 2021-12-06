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
    public partial class AdminLoginPage : ContentPage
    {
        private string adminLogin = "admin";
        private string adminPassword = "admin";
        public AdminLoginPage()
        {
            InitializeComponent();
        }
        private async void bLogin_Clicked(object sender, EventArgs e)
        {
            if (eLogin.Text != adminLogin || ePassword.Text != adminPassword)
            {
                await DisplayAlert("Ошибка", "Неверный логин или пароль", "Ок");
                return;
            }
            Application.Current.MainPage = new AdminShell();
        }

        private void showPaswordSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (showPaswordSwitch.IsToggled == true)
                ePassword.IsPassword = false;
            else
                ePassword.IsPassword = true;
        }
    }
}