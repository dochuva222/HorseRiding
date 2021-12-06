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
    public partial class UserMessagesPage : ContentPage
    {
        public UserMessagesPage()
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
            lvMessages.ItemsSource = await APIManager.GetData<List<UserMessage>>($"UserMessagesByUserId/{UserShell.LoggedUser.Id}");
            refreshView.IsRefreshing = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }
    }
}