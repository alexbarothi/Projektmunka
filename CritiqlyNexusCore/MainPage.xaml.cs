using System.Text.Json;
using CritiqlyNexusCore.Models;
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

            GetMovies();
            GetAdminData();
        }

        public async void GetAdminData()
        {
            var client = new HttpClient();

            var data = await client.GetAsync("http://127.0.0.1:8000/api/admin/get");
            var json = await data.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<AdminData>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            await DisplayAlertAsync("DEBUG", result[0].DailyLastUpdate + " - " + result[0].TrendingLastUpdate, "OK");

            AppData.DailyLastUpdated = result[0].DailyLastUpdate;
            AppData.TrendingLastUpdated = result[0].TrendingLastUpdate;

        }

        public async void GetMovies()
        {

        }

        public async void GetRatings()
        {

        }

        public async void LogOut(Object sender, EventArgs e)
        {

        }
    }
}
