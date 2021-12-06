using HorseRiding.Models;
using HorseRiding.Services;
using HorseRiding.Views.UserViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding.Views.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserLoginPage : ContentPage
    {
        public UserLoginPage()
        {
            InitializeComponent();
        }

        private async void bLogin_Clicked(object sender, EventArgs e)
        {
            var loggedUser = await APIManager.GetData<User>($"Users?phone={eLogin.Text}");
            if (loggedUser == null || loggedUser.Password != GetHash(ePassword.Text))
            {
                await DisplayAlert("Ошибка", "Неверный номер телефона или пароль", "Ок");
                return;
            }
            UserShell.LoggedUser = loggedUser;
            Application.Current.MainPage = new UserShell();
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        private async void bRegistration_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
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