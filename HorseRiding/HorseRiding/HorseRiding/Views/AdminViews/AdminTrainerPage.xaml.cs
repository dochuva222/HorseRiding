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

namespace HorseRiding.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(SentTrainerId), nameof(SentTrainerId))]
    public partial class AdminTrainerPage : ContentPage
    {
        public string SentTrainerId { get; set; }
        Trainer contextTrainer;

        public AdminTrainerPage()
        {
            InitializeComponent();
            LoadTypes();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private async void LoadTypes()
        {
            pTrainingTypes.ItemsSource = await APIManager.GetData<List<TrainingType>>("TrainingTypes");
        }

        private async void Refresh()
        {
            if (SentTrainerId != "0")
                contextTrainer = await APIManager.GetData<Trainer>($"Trainers/{SentTrainerId}");
            else
                contextTrainer = new Trainer() { ExperienceDate = DateTime.Now };
            BindingContext = contextTrainer;
            pTrainingTypes.SelectedIndex = contextTrainer.TrainingTypeId - 1;
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
                contextTrainer.Image = memoryStream.ToArray();
                trainerImage.Source = contextTrainer.TrainerImage;
            }
        }

        private async void bSave_Clicked(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (contextTrainer.Image == null)
                errorMessage += "Выберите фото\n";
            if (string.IsNullOrWhiteSpace(contextTrainer.FullName))
                errorMessage += "Введите фамилию и имя\n";
            if (pTrainingTypes.SelectedItem == null)
                errorMessage += "Выберите курс\n";
            if (string.IsNullOrWhiteSpace(contextTrainer.Phone) || contextTrainer.Phone.Length < 11)
                errorMessage += "Введите корректный номер телефона\n";
            if (string.IsNullOrWhiteSpace(ePassword.Text))
                errorMessage += "Введите пароль\n";
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                await DisplayAlert("Ошибка", errorMessage, "Ок");
                return;
            }
            contextTrainer.TrainingTypeId = pTrainingTypes.SelectedIndex + 1;
            contextTrainer.Password = GetHash(ePassword.Text);
            if (contextTrainer.Id == 0)
                await APIManager.PostData("Trainers", contextTrainer);
            else
                await APIManager.PutData($"PutTrainers/{contextTrainer.Id}", contextTrainer);
            await Shell.Current.GoToAsync("..");
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}