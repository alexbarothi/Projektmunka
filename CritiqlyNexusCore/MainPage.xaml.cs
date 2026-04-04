using Microsoft.Maui.Platform;

namespace CritiqlyNexusCore
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            welcomeLabel.Text = "Üdv újra, " + AppData.Username + "!";
        }

        public async void GetMovies(Object sender, EventArgs e)
        {

        }

        public async void GetRatings(Object sender, EventArgs e)
        {

        }

        public async void LogOut(Object sender, EventArgs e)
        {

        }
    }
}
