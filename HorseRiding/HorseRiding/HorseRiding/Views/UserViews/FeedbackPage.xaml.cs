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
    public partial class FeedbackPage : ContentPage
    {
        Feedback contextFeedback;
        public FeedbackPage()
        {
            InitializeComponent();
            LoadFeedback();
        }
        private async void LoadFeedback()
        {
            var feedback = await APIManager.GetData<Feedback>($"Feedbacks?userId={UserShell.LoggedUser.Id}");
            if (feedback == null)
                contextFeedback = new Feedback() { UserId = UserShell.LoggedUser.Id };
            else
                contextFeedback = feedback;
            BindingContext = contextFeedback;
            ratingBar.Rating = contextFeedback.Rating;
            
        }
        private async void bCancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void bSave_Clicked(object sender, EventArgs e)
        {
            contextFeedback.Date = DateTime.Now;
            contextFeedback.Rating = ratingBar.Rating;
            if (contextFeedback.Id != 0)
                await APIManager.PutData($"Feedbacks/{contextFeedback.Id}", contextFeedback);
            else
                await APIManager.PostData("Feedbacks", contextFeedback);
            await Shell.Current.GoToAsync("..");
        }
    }
}