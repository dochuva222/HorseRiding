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
    public partial class AdminRentsPage : ContentPage
    {
        public AdminRentsPage()
        {
            InitializeComponent();
        }

        private void RefreshView_Refreshing(object sender, EventArgs e)
        {
            Refresh();
        }

        private async void Refresh()
        {
            refreshView.IsRefreshing = true;
            lvRents.ItemsSource = await APIManager.GetData<List<RentRecord>>("ActualRentRecords");
            refreshView.IsRefreshing = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private async void bDelete_Clicked(object sender, EventArgs e)
        {
            var selectedRent = (sender as Button).BindingContext as RentRecord;
            var messageContent = $"Прокат {selectedRent.RentDate.ToShortDateString()} {selectedRent.RentTime} отменен";
            await Shell.Current.GoToAsync($"{nameof(SendMessagePage)}?SentUserId={selectedRent.UserId}&MessageContent={messageContent}&RentId={selectedRent.Id}");
        }
    }
}