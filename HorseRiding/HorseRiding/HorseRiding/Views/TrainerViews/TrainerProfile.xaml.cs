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
    public partial class TrainerProfile : ContentPage
    {
        public static Trainer LoggedTrainer;
        public TrainerProfile()
        {
            InitializeComponent();
            BindingContext = LoggedTrainer;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrainerCalendarPage());
        }

        private async void bSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ePassword.Text))
            {
                await DisplayAlert("Ошибка", "Введите пароль", "Ок");
                return;
            }
            LoggedTrainer.Password = GetHash(ePassword.Text);
            await APIManager.PutData($"PutTrainers/{LoggedTrainer.Id}", LoggedTrainer);
            ePassword.Text = "";
            await DisplayAlert ("", "Данные успешно изменены", "Ок");
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new ChooseRolePage());
        }
    }
}