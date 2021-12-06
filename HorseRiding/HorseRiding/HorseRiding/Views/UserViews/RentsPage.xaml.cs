using HorseRiding.Models;
using HorseRiding.Services;
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
    public partial class RentsPage : ContentPage
    {
        public RentsPage()
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
            var rents = await APIManager.GetData<List<RentRecord>>($"RentRecords?userId={UserShell.LoggedUser.Id}");
            lvRents.ItemsSource = rents.OrderBy(r => r.RentDate).ToList();
            refreshView.IsRefreshing = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RentPage));
        }

        private void bDelete_Clicked(object sender, EventArgs e)
        {
            APIManager.DeleteData($"RentRecords/{((sender as Button).BindingContext as RentRecord).Id}");
            Refresh();
        }
    }
}