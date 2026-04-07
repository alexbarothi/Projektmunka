using System.Collections.ObjectModel;
using System.Text.Json;
using CritiqlyNexusCore.Models;

namespace CritiqlyNexusCore;

public partial class DailyPage : ContentPage
{
    public ObservableCollection<Movie> QueryMovies { get; set; } = new ObservableCollection<Movie>();
    public List<DailyMovie> DailyMovies = new List<DailyMovie>();
    public List<int> CurrentDayIds = new();

    public DailyPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
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

    public async void RollRandom(Object sender, EventArgs e)
    {
        rollRandomBtn.BackgroundColor = Colors.Orange;
        int count = CurrentDayIds.Count;
        int currentNum = 0;
        if (count != 15)
        {
            currentNum = 15 - count;

            Random rnd = new Random();
            for (int i = 0; i < currentNum; i++)
            {
                CurrentDayIds.Add(rnd.Next(1, (AppData.Movies.Count + 1)));
                await Task.Delay(500);
                rollRandomBtn.BackgroundColor = Color.FromRgb(212, 255, 62);
            }
        }
        else
        {
            await DisplayAlertAsync("INFO", "Elérted a maximálisan hozzáadható filmek számát!", "OK");
        }
    }

    public async void AddToDailyList(Object sender, EventArgs e)
    {

    }

    public async void checkSelected(Object sender, EventArgs e)
    {

    }

    public async void getDailys()
    {
        DailyMovies.Clear();
        var client = new HttpClient();

        var response = await client.GetAsync("http://localhost:8000/api/get-daily");

        if (!response.IsSuccessStatusCode)
            throw new Exception("API error");

        var json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<List<DailyMovie>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        foreach (var dailymovie in result)
        {
            DailyMovies.Add(dailymovie);
        }
    }

    public async void LoadDailys(object sender, DateChangedEventArgs e)
    {

    }
    public async void Exit(Object sender, EventArgs e)
    {

    }

    public async void Save(Object sender, EventArgs e)
    {

    }
}