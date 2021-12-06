using HorseRiding.Models;
using HorseRiding.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding.Views.UserViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainingPage : ContentPage
    {
        UserTraining contextUserTraining;
        public TrainingPage()
        {
            InitializeComponent();
            dpDate.Date = DateTime.Now;
            dpDate.MinimumDate = DateTime.Now;
            LoadTypes();
            List<string> times = new List<string>();
            for (int i = 9; i <= 15; i++)
            {
                if (i == 13)
                    continue;
                var time = $"{TimeSpan.FromHours(i)} - {TimeSpan.FromHours(i).Add(TimeSpan.FromMinutes(45))}";
                times.Add(time);
            }
            pTimes.ItemsSource = times;
            contextUserTraining = new UserTraining() { UserId = UserShell.LoggedUser.Id };
            MessagingCenter.Subscribe<TrainingPage, Horse>(this, "HorseChange",
                (sender, Horse) =>
                {
                    contextUserTraining.HorseId = Horse.Id;
                    horseFrame.BindingContext = Horse;
                });
            MessagingCenter.Subscribe<TrainingPage, Trainer>(this, "TrainerChange",
                (sender, Trainer) =>
                {
                    contextUserTraining.TrainerId = Trainer.Id;
                    trainerFrame.BindingContext = Trainer;
                });
        }

        private async void LoadTypes()
        {
            var types = await APIManager.GetData<List<TrainingType>>("TrainingTypes");
            pTrainingTypes.ItemsSource = types.Where(t => t.Id != 4).ToList();
        }

        private async void bSave_Clicked(object sender, EventArgs e)
        {
            var selectedTrainingType = pTrainingTypes.SelectedItem as TrainingType;
            var selectedDate = dpDate.Date;
            var selectedTime = TimeSpan.FromHours(int.Parse($"{pTimes.SelectedItem.ToString()[0]}{pTimes.SelectedItem.ToString()[1]}"));
            var currentDateTraining = await APIManager.GetData<UserTraining>($"UserTrainings?date={selectedDate.Date.Ticks}&time={selectedTime.Ticks}");
            var selectedDateRent = await APIManager.GetData<RentRecord>($"RentRecords?userId={UserShell.LoggedUser.Id}&date={selectedDate.Date.Ticks}");
            var selectedDateTraining = await APIManager.GetData<UserTraining>($"UserTrainings?userId={UserShell.LoggedUser.Id}&date={selectedDate.Date.Ticks}");
            var horseIsFree = await APIManager.GetData<bool>($"Horses?horseId={contextUserTraining.HorseId}&date={selectedDate.Ticks}&time={selectedTime.Ticks}");
            var trainerIsFree = await APIManager.GetData<bool>($"Trainers?trainerId={contextUserTraining.TrainerId}&date={selectedDate.Ticks}&time={selectedTime.Ticks}");

            if (selectedTrainingType == null)
            {
                await DisplayAlert("Ошибка", "Выберите курс", "Ок");
                return;
            }

            if (selectedDate < DateTime.Now.Date)
            {
                await DisplayAlert("Ошибка", "Неверная дата", "Ок");
                return;
            }

            if (pTimes.SelectedItem == null)
            {
                await DisplayAlert("Ошибка", "Выберите время", "Ок");
                return;
            }

            if (selectedDate.Date == DateTime.Now.Date && selectedTime < DateTime.Now.TimeOfDay)
            {
                await DisplayAlert("Ошибка", "Неверное время", "Ок");
                return;
            }

            if (currentDateTraining != null)
            {
                await DisplayAlert("Ошибка", "На выбранную дату уже запланирована тренировка", "Ок");
                return;
            }

            if (contextUserTraining.TrainerId == 0)
            {
                await DisplayAlert("Ошибка", "Выберите тренера", "Ок");
                return;
            }

            if (!trainerIsFree)
            {
                await DisplayAlert("Ошибка", "На это время тренер уже занят", "Ок");
                return;
            }

            if(contextUserTraining.HorseId == 0)
            {
                await DisplayAlert("Ошибка", "Выберите лошадь", "Ок");
                return;
            }

            if (!horseIsFree)
            {
                await DisplayAlert("Ошибка", "На это время лошадь уже занята", "Ок");
                return;
            }

            if (selectedDateRent != null)
            {
                await DisplayAlert("Ошибка", "На эту дату у вас уже записан прокат", "Ок");
                return;
            }

            if (selectedDateTraining != null)
            {
                await DisplayAlert("Ошибка", "На эту дату у вас уже записана тренировка", "Ок");
                return;
            }

            contextUserTraining.TrainingTypeId = selectedTrainingType.Id;
            contextUserTraining.TrainingDate = selectedDate.Date.AddHours(12);
            contextUserTraining.TrainingTime = selectedTime;
            await APIManager.PostData("UserTrainings", contextUserTraining);
            await DisplayAlert("Успешно", "Вы успешно записались на тренировку, оплата производится на месте", "Вернуться");
            await Shell.Current.GoToAsync("..");
        }

        private async void bChooseHorse_Clicked(object sender, EventArgs e)
        {
            var selectedTrainingType = pTrainingTypes.SelectedItem as TrainingType;
            if (selectedTrainingType == null)
            {
                await DisplayAlert("Ошибка", "Выберите тип тренировки", "Ок");
                return;
            }
            await Shell.Current.GoToAsync($"{nameof(HorsesPage)}?SentPage=Course&TrainingTypeId={selectedTrainingType.Id}");
        }

        private async void bChooseTrainer_Clicked(object sender, EventArgs e)
        {
            var selectedTrainingType = pTrainingTypes.SelectedItem as TrainingType;
            if(selectedTrainingType == null)
            {
                await DisplayAlert("Ошибка", "Выберите тип тренировки", "Ок");
                return;
            }
            await Shell.Current.GoToAsync($"{nameof(TrainersPage)}?TrainingTypeId={selectedTrainingType.Id}");
        }

        private void pTrainingTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            contextUserTraining.HorseId = 0;
            contextUserTraining.TrainerId = 0;
            horseFrame.BindingContext = null;
            trainerFrame.BindingContext = null;
            lCost.Text = $"Стоимость: {(pTrainingTypes.SelectedItem as TrainingType).Cost} р.";
        }

        private async void bCancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}