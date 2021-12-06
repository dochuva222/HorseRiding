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
    public partial class RentPage : ContentPage
    {
        RentRecord contextRentRecord;
        public RentPage()
        {
            InitializeComponent();
            dpDate.Date = DateTime.Now;
            dpDate.MinimumDate = DateTime.Now;
            List<string> times = new List<string>();
            for (int i = 9; i <= 15; i++)
            {
                if (i == 13)
                    continue;
                var time = $"{TimeSpan.FromHours(i)} - {TimeSpan.FromHours(i).Add(TimeSpan.FromMinutes(45))}";
                times.Add(time);
            }
            List<int> horses = new List<int>();
            for (int i = 1; i <= 2; i++)
            {
                horses.Add(i);
            }
            pHorses.ItemsSource = horses;
            pTimes.ItemsSource = times;
            contextRentRecord = new RentRecord() { UserId = UserShell.LoggedUser.Id };
            BindingContext = contextRentRecord;
            MessagingCenter.Subscribe<TrainingPage, Trainer>(this, "TrainerChange",
               (sender, Trainer) =>
               {
                   contextRentRecord.TrainerId = Trainer.Id;
                   trainerFrame.BindingContext = Trainer;
               });
        }

        private async void bRent_Clicked(object sender, EventArgs e)
        {
            var selectedDate = dpDate.Date;
            var selectedTime = TimeSpan.FromHours(int.Parse($"{pTimes.SelectedItem.ToString()[0]}{pTimes.SelectedItem.ToString()[1]}"));
            var horses = (int)pHorses.SelectedItem;
            var currentDateRentRecord = await APIManager.GetData<RentRecord>($"RentRecords?date={selectedDate.Date.Ticks}&time={selectedTime.Ticks}");
            var selectedDateRent = await APIManager.GetData<RentRecord>($"RentRecords?userId={UserShell.LoggedUser.Id}&date={selectedDate.Date.Ticks}");
            var selectedDateTraining = await APIManager.GetData<UserTraining>($"UserTrainings?userId={UserShell.LoggedUser.Id}&date={selectedDate.Date.Ticks}");
            var isTrainerFree = await APIManager.GetData<bool>($"RentTrainerIsFree?id={contextRentRecord.TrainerId}&date={selectedDate.Date.Ticks}&time={selectedTime.Ticks}");

            if (selectedDate < DateTime.Now.Date)
            {
                await DisplayAlert("Ошибка", "Неверная дата", "Ок");
                return;
            }
            
            if(pTimes.SelectedItem == null)
            {
                await DisplayAlert("Ошибка", "Выберите время", "Ок");
                return;
            }

            if (selectedDate.Date == DateTime.Now.Date && selectedTime < DateTime.Now.TimeOfDay)
            {
                await DisplayAlert("Ошибка", "Неверное время", "Ок");
                return;
            }

            if (pHorses.SelectedItem == null)
            {
                await DisplayAlert("Ошибка", "Выберите количество лошадей", "Ок");
                return;
            }

            if (contextRentRecord.TrainerId == 0)
            {
                await DisplayAlert("Ошибка", "Выберите тренера", "Ок");
                return;
            }

            if (isTrainerFree == false)
            {
                await DisplayAlert("Ошибка", "На это время тренер занят", "Ок");
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

            if (currentDateRentRecord != null)
            {
                await DisplayAlert("Ошибка", "На это время уже записан прокат", "Ок");
                return;
            }

            contextRentRecord.HorseQuantity = horses;
            contextRentRecord.RentDate = selectedDate.Date.AddHours(12);
            contextRentRecord.RentTime = selectedTime;
            await APIManager.PostData("RentRecords", contextRentRecord);
            await DisplayAlert("Успешно", "Вы успешно записались на прокат, оплата производится на месте", "Вернуться");
            await Shell.Current.GoToAsync("..");
        }
        private async void bChooseTrainer_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(TrainersPage)}?TrainingTypeId=4");
        }

        private async void bCancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void bHorses_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(HorsesPage)}?SentPage=Rent?TrainingTypeId={null}");
        }
    }
}