using HorseRiding.Models;
using HorseRiding.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding.Views.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        User contextUser;
        bool isPickPhoto = false;
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (isPickPhoto == false)
                LoadUser();
            isPickPhoto = false;
        }
        private async void LoadUser()
        {
            contextUser = await APIManager.GetData<User>($"Users?phone={UserShell.LoggedUser.Phone}");
            BindingContext = contextUser;
            userImage.Source = contextUser.UserImage;
        }

        private async void bSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contextUser.Firstname) || string.IsNullOrWhiteSpace(contextUser.Lastname))
            {
                await DisplayAlert ("Ошибка", "Пустое имя или фамилия", "Ок");
                return;
            }
            if (string.IsNullOrWhiteSpace(ePassword.Text))
            {
                await DisplayAlert("Ошибка", "Введите пароль", "Ок");
                return;
            }
            contextUser.Password = GetHash(ePassword.Text);
            await APIManager.PutData($"Users/{contextUser.Id}", contextUser);
            ePassword.Text = "";
            await DisplayAlert("", "Данные успешно изменены", "Ок");
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        private async void bChangePhoto_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "Выберите фото"
            });
            if (result == null)
                return;
            var stream = await result.OpenReadAsync();
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                contextUser.Image = memoryStream.ToArray();
                userImage.Source = contextUser.UserImage;
            }
            isPickPhoto = true;
        }
    }
}