using HorseRiding.Models;
using HorseRiding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding.Views.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        User contextUser;
        public RegistrationPage()
        {
            InitializeComponent();
            contextUser = new User();
            BindingContext = contextUser;
        }

        private async void bRegistration_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contextUser.Phone) || !Regex.IsMatch(contextUser.Phone, "[0-9]{11}"))
            {
                await DisplayAlert("Ошибка", "Введите номер телефона", "Ок");
                return;
            }
            if (string.IsNullOrWhiteSpace(contextUser.Password))
            {
                await DisplayAlert("Ошибка", "Введите пароль", "Ок");
                return;
            }
            if (string.IsNullOrWhiteSpace(contextUser.Firstname))
            {
                await DisplayAlert("Ошибка", "Введите имя", "Ок");
                return;
            }
            if (string.IsNullOrWhiteSpace(contextUser.Lastname))
            {
                await DisplayAlert("Ошибка", "Введите фамилию", "Ок");
                return;
            }
            var user = await APIManager.GetData<User>($"Users?phone={contextUser.Phone}");
            if (user != null)
            {
                await DisplayAlert("Ошибка", "Данный номер телефона уже используется", "Ок");
                return;
            }
            contextUser.Password = GetHash(contextUser.Password);
            await APIManager.PostData("Users", contextUser);
            await Navigation.PopAsync();
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        private async void bCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}