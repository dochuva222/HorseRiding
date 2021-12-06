using HorseRiding.Views.AdminViews;
using HorseRiding.Views.TrainerViews;
using HorseRiding.Views.UserViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorseRiding.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseRolePage : ContentPage
    {
        public ChooseRolePage()
        {
            InitializeComponent();
        }

        private void bUser_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserLoginPage());
        }

        private void bTrainer_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TrainerLoginPage());
        }

        private void bAdmin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdminLoginPage());
        }
    }
}