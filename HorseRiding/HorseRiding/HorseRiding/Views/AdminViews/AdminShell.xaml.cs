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
    public partial class AdminShell : Xamarin.Forms.Shell
    {
        public AdminShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AdminTrainerPage), typeof(AdminTrainerPage));
            Routing.RegisterRoute(nameof(AdminHorsePage), typeof(AdminHorsePage));
            Routing.RegisterRoute(nameof(SendMessagePage), typeof(SendMessagePage));

        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new ChooseRolePage());
        }
    }
}