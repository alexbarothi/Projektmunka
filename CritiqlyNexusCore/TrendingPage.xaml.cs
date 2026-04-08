using System.Collections.ObjectModel;
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

    }

    public async void SearchQuery(Object sender, EventArgs e)
    {

    }

    public async void autoSelect(Object sender, EventArgs e)
    {

    }
    public async void AddToTrendingList(Object sender, EventArgs e)
    {

    }

    public async void checkSelected(Object sender, EventArgs e)
    {

    }

    public async void Exit(Object sender, EventArgs e)
    {
        var isResponseOk = await DisplayAlertAsync(
            "Kilépés",
            "Biztosan ki akarsz lépni? Az eddigi változtatások elvesznek!",
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

    }
}