using HorseRiding.Models;
using HorseRiding.Services;
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
    [QueryProperty(nameof(SentUserId), nameof(SentUserId))]
    [QueryProperty(nameof(MessageContent), nameof(MessageContent))]
    [QueryProperty(nameof(RentId), nameof(RentId))]
    [QueryProperty(nameof(TrainingId), nameof(TrainingId))]
    public partial class SendMessagePage : ContentPage
    {
        public string SentUserId { get; set; }
        public string MessageContent { get; set; }
        public string RentId { get; set; }
        public string TrainingId { get; set; }
        public SendMessagePage()
        {
            InitializeComponent();
        }

        private async void bSend_Clicked(object sender, EventArgs e)
        {
            UserMessage message = new UserMessage() { UserId = int.Parse(SentUserId), Date = DateTime.Now, Message = $"{MessageContent} \nПричина: {eMessage.Text}" };
            await APIManager.PostData("UserMessages", message);
            if (RentId != null)
                APIManager.DeleteData($"RentRecords/{RentId}");
            else
                APIManager.DeleteData($"UserTrainings/{TrainingId}");
            await Shell.Current.GoToAsync("..");
        }
    }
}