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

    }

    public async void RollRandom(Object sender, EventArgs e)
    {

    }

    public async void AddToDailyList(Object sender, EventArgs e)
    {

    }

    public async void checkSelected(Object sender, EventArgs e)
    {

    }

    public async void getDailys()
    {

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