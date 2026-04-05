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
            GetRatings();
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
            var ratings = await GetAsync<Rating>("http://127.0.0.1:8000/api/ratings");

            AppData.Ratings.Clear();

            foreach (var rating in ratings)
            {
                AppData.Ratings.Add(rating);
            }

            //fireUp();
            getRatingsBtn.BackgroundColor = Colors.DarkGreen;
            getRatingsBtn.Text = "ÉRTÉKELÉSEK ✓";
            //getMoviesBtn.TextColor = Colors.Black;
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

        public async void FireUp()
        {
            if (AppData.Movies.Count > 0 && AppData.Ratings.Count > 0)
            {
                DailyMenuBtn.IsEnabled = true;
                TrendingMenuBtn.IsEnabled = true;
                TrendingMenuBtn.IsEnabled = true;
                DeleteMenuBtn.IsEnabled = true;

                DailyMenuBtn.BackgroundColor = Color.FromRgb(212, 255, 62);
                TrendingMenuBtn.BackgroundColor = Color.FromRgb(212, 255, 62);
                UpdateMenuBtn.BackgroundColor = Color.FromRgb(212, 255, 62);
                DeleteMenuBtn.BackgroundColor = Color.FromRgb(212, 255, 62);

                DailyMenuBtn.FadeTo(1, 1000);
                TrendingMenuBtn.FadeTo(1, 1000);
                UpdateMenuBtn.FadeTo(1, 1000);
                DeleteMenuBtn.FadeTo(1, 1000);
            }
        }

        public async void LogOut(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
