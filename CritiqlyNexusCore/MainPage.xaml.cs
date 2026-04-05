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

            //await DisplayAlertAsync("DEBUG", result[0].DailyLastUpdate + " - " + result[0].TrendingLastUpdate, "OK");

            AppData.DailyLastUpdated = result[0].DailyLastUpdate;
            AppData.TrendingLastUpdated = result[0].TrendingLastUpdate;

        }

        public async void GetMovies()
        {
            var movies = await GetAsync<Movie>("http://127.0.0.1:8000/api/movies");

            AppData.Movies.Clear();

            foreach (var movie in movies)
            {
                AppData.Movies.Add(movie);
            }

            //fireUp();
            getMoviesBtn.BackgroundColor = Colors.DarkGreen;
            getMoviesBtn.Text = "FILMEK ✓";
            //getMoviesBtn.TextColor = Colors.Black;
        }

        public async void GetRatings()
        {

        }

        public async Task<List<T>> GetAsync<T>(string url)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("API error");

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null)
                throw new Exception("Deserialization failed");

            return result;
        }

        public async void LogOut(Object sender, EventArgs e)
        {

        }
    }
}
