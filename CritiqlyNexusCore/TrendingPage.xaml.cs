using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using CritiqlyNexusCore.Models;

namespace CritiqlyNexusCore;

public partial class TrendingPage : ContentPage
{
    public ObservableCollection<Movie> QueryMovies { get; set; } = new ObservableCollection<Movie>();
    public List<int> SelectedIds = new List<int>();
    public TrendingPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        EntryQuery.Text = "";
        StatusLabel.Text = "Utolsó frissítés: " + AppData.TrendingLastUpdated?.ToString("yyyy.MM.dd HH:mm") ?? "N/I";

        SelectedIds.Clear();
        QueryMovies.Clear();

    }

    public async void SearchQuery(Object sender, EventArgs e)
    {
        searchQueryBtn.BackgroundColor = Colors.Orange;
        QueryMovies.Clear();
        foreach (var movie in AppData.Movies)
        {
            if (movie.title.ToLower().Contains(EntryQuery.Text.ToLower()))
            {
                QueryMovies.Add(movie);
            }
        }
        await Task.Delay(1000);
        searchQueryBtn.BackgroundColor = Color.FromRgb(212, 255, 62);
    }

    public async void autoSelect(Object sender, EventArgs e)
    {
        Dictionary<int, int> totalStars = new Dictionary<int, int>();
        Dictionary<int, int> voteCounts = new Dictionary<int, int>();

        foreach (var rating in AppData.Ratings)
        {
            if (!totalStars.ContainsKey(rating.movie_id))
            {
                totalStars[rating.movie_id] = 0;
                voteCounts[rating.movie_id] = 0;
            }

            totalStars[rating.movie_id] += rating.stars;
            voteCounts[rating.movie_id]++;
        }

        var tops = totalStars
        .Select(x => new {
            MovieId = x.Key,
            Avg = (double)x.Value / voteCounts[x.Key]
        })
        .OrderByDescending(x => x.Avg)
        .Take(15)
        .Select(x => x.MovieId)
        .ToArray();

        for (int i = 0; i < tops.Length; i++)
        {
            SelectedIds.Add(tops[i]);
        }

        checkSelected(this, EventArgs.Empty);
    }
    public async void AddToTrendingList(Object sender, EventArgs e)
    {
        var Button = sender as Button;
        var id = Button?.CommandParameter;

        if (SelectedIds.Count <= 15 && !SelectedIds.Contains((Int32)id))
        {
            Button.BackgroundColor = Colors.Orange;
            SelectedIds.Add((Int32)id);
            AppData.Movies.First(x => x.id == (Int32)id).isSelectedTrending = true;
            await Task.Delay(500);
            Button.BackgroundColor = Color.FromRgb(212, 255, 62);
        }
        else if (SelectedIds.Contains((Int32)id))
        {
            Button.BackgroundColor = Colors.Red;
            SelectedIds.Remove((Int32)id);
            AppData.Movies.First(x => x.id == (Int32)id).isSelectedTrending = false;
            await Task.Delay(500);
            Button.BackgroundColor = Colors.Orange;
            checkSelected(this, EventArgs.Empty);
        }
        else
        {
            await DisplayAlertAsync("INFO", "Elérted a maximálisan hozzáadható filmek számát!", "OK");
        }
    }

    public async void checkSelected(Object sender, EventArgs e)
    {
        QueryMovies.Clear();
        checkSelectedBtn.BackgroundColor = Colors.Orange;

        foreach (var movie in AppData.Movies)
        {
            if (SelectedIds.Contains(movie.id))
            {
                QueryMovies.Add(movie);
            }
        }
        await Task.Delay(500);
        checkSelectedBtn.BackgroundColor = Color.FromRgb(212, 255, 62);
    }

    public async void Exit(Object sender, EventArgs e)
    {
        var isResponseOk = await DisplayAlertAsync(
            "Kilépés",
            "Biztosan ki akarsz lépni? A nem mentett változtatások elvesznek!",
            "Igen",
            "Mégse"
        );

        if (isResponseOk)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            return;
        }
    }

    public async void Save(Object sender, EventArgs e)
    {
        if (SelectedIds.Count > 2)
        {
            var client = new HttpClient();

            var trendingData = new
            {
                movies = SelectedIds.ToArray(),
            };
            var trendingJson = JsonSerializer.Serialize(trendingData);
            var httpTrendingData = new StringContent(trendingJson, Encoding.UTF8, "application/json");

            var responseDaily = await client.PostAsync("http://localhost:8000/api/trending-movies", httpTrendingData);

            var dateData = new
            {
                daily = AppData.DailyLastUpdated.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                trending = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            };
            var dateJson = JsonSerializer.Serialize(dateData);
            var httpDateDate = new StringContent(dateJson, Encoding.UTF8, "application/json");
            //await DisplayAlertAsync("json", json, "OK");
            var responseDate = await client.PostAsync("http://localhost:8000/api/admin/update", httpDateDate);

            if (responseDate.IsSuccessStatusCode && responseDaily.IsSuccessStatusCode)
            {
                AppData.TrendingLastUpdated = DateTime.Now;
                StatusLabel.Text = "Utolsó frissítés: " + AppData.TrendingLastUpdated?.ToString("yyyy.MM.dd HH:mm") ?? "N/I";
                await Shell.Current.GoToAsync($"//MainPage");
            }
            else
            {
                await DisplayAlertAsync("Hiba", "A mentés nem sikerült! \n Próbáld újra!", "OK");
            }
        }
        else
        {
            await DisplayAlertAsync("HIBA", "A feltölteni kívánt filmek száma nem elegendő!", "OK");
        }
    }
}