using HorseRiding.Models;
using HorseRiding.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(SentHorseId), nameof(SentHorseId))]

    public partial class AdminHorsePage : ContentPage
    {
        public string SentHorseId { get; set; }
        Horse contextHorse;

        public AdminHorsePage()
        {
            InitializeComponent();
            LoadTypes();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private async void Refresh()
        {
            if (SentHorseId != "0")
                contextHorse = await APIManager.GetData<Horse>($"Horses/{SentHorseId}");
            else
                contextHorse = new Horse();
            BindingContext = contextHorse;
            if (contextHorse.TrainingTypeId.HasValue)
                pTrainingTypes.SelectedIndex = contextHorse.TrainingTypeId.Value - 1;
        }

        private async void LoadTypes()
        {
            pTrainingTypes.ItemsSource = await APIManager.GetData<List<TrainingType>>("TrainingTypes");
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
                contextHorse.Image = memoryStream.ToArray();
                horseImage.Source = contextHorse.HorseImage;
            }
        }

        private async void bSave_Clicked(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (contextHorse.Image == null)
                errorMessage += "Выберите фото\n";
            if (string.IsNullOrWhiteSpace(contextHorse.Name))
                errorMessage += "Введите имя\n";
            if (pTrainingTypes.SelectedItem == null)
                errorMessage += "Выберите курс\n";
            if (string.IsNullOrWhiteSpace(contextHorse.Description))
                errorMessage += "Введите описание\n";
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                await DisplayAlert("Ошибка", errorMessage, "Ок");
                return;
            }
            contextHorse.TrainingTypeId = pTrainingTypes.SelectedIndex + 1;
            if (contextHorse.Id == 0)
                await APIManager.PostData("PostHorses", contextHorse);
            else
                await APIManager.PutData($"PutHorses/{contextHorse.Id}", contextHorse);
            await Shell.Current.GoToAsync("..");
        }
    }
}