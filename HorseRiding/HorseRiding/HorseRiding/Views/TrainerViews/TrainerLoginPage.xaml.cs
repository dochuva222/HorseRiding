using HorseRiding.Models;
using HorseRiding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding.Views.TrainerViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainerLoginPage : ContentPage
    {
        public TrainerLoginPage()
        {
            InitializeComponent();
        }

        private async void bLogin_Clicked(object sender, EventArgs e)
        {
            var loggedTrainer = await APIManager.GetData<Trainer>($"Trainers?phone={eLogin.Text}");
            if (loggedTrainer == null || loggedTrainer.Password != GetHash(ePassword.Text))
            {
                await DisplayAlert("Ошибка", "Неверный номер телефона или пароль", "Ок");
                return;
            }
            TrainerProfile.LoggedTrainer = loggedTrainer;
            await Navigation.PushAsync(new TrainerProfile());
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
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