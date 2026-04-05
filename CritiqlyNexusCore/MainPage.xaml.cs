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
            getMoviesBtn.IsEnabled = false;
            var movies = await GetAsync<Movie>("http://127.0.0.1:8000/api/movies", "movie");

            AppData.Movies.Clear();

            if(movies.Count > 0)
            {
                getMoviesBtn.BackgroundColor = Colors.DarkGreen;
                getMoviesBtn.Text = "FILMEK ✓";
                //getMoviesBtn.TextColor = Colors.Black;

                foreach (var movie in movies)
                {
                    AppData.Movies.Add(movie);
                }

                FireUp();
            }


        }

        public async void GetRatings()
        {
            getRatingsBtn.IsEnabled = false;
            var ratings = await GetAsync<Rating>("http://127.0.0.1:8000/api/ratings", "rating");

            AppData.Ratings.Clear();

            if(ratings.Count > 0)
            {
                getRatingsBtn.BackgroundColor = Colors.DarkGreen;
                getRatingsBtn.Text = "ÉRTÉKELÉSEK ✓";
                //getMoviesBtn.TextColor = Colors.Black;

                foreach (var rating in ratings)
                {
                    AppData.Ratings.Add(rating);
                }

                FireUp();
            }
        }

        public async Task<List<T>> GetAsync<T>(string url, string type)
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    HandleError(type);
                    return new List<T>();
                }

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result == null)
                {
                    HandleError(type);
                    return new List<T>();
                }

                return result;
            }
            catch
            {
                HandleError(type);
                return new List<T>();
            }
        }

        private void HandleError(string type)
        {
            if (type == "movie")
            {
                getMoviesBtn.BackgroundColor = Color.FromRgb(105, 29, 12);
                getMoviesBtn.Text = "FILMEK ⨯";
                getMoviesBtn.IsEnabled = true;
            }
            else if (type == "rating")
            {
                getRatingsBtn.BackgroundColor = Color.FromRgb(105, 29, 12);
                getRatingsBtn.Text = "ÉRTÉKELÉSEK ⨯";
                getRatingsBtn.IsEnabled= true;
            }
        }

        public async void FireUp()
        {
            if (AppData.Movies.Count > 0 && AppData.Ratings.Count > 0)
            {
                DailyMenuBtn.IsEnabled = true;
                TrendingMenuBtn.IsEnabled = true;
                TrendingMenuBtn.IsEnabled = true;
                DeleteMenuBtn.IsEnabled = true;

                DailyMenuBtn.FadeTo(1, 1000);
                TrendingMenuBtn.FadeTo(1, 1000);
                UpdateMenuBtn.FadeTo(1, 1000);
                DeleteMenuBtn.FadeTo(1, 1000);
            }
        }

        public async void OpenDailyPage(Object sender, EventArgs e)
        {

        }

        public async void OpenTrendingPage(Object sender, EventArgs e)
        {

        }

        public async void OpenUpdatePage(Object sender, EventArgs e)
        {

        }

        public async void OpenDeletePage(Object sender, EventArgs e)
        {

        }

        public async void LogOut(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
