using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text;
using CritiqlyNexusCore.Models;

namespace CritiqlyNexusCore;

public partial class DeletePage : ContentPage
{
    public ObservableCollection<Movie> QueryMovies { get; set; } = new ObservableCollection<Movie>();
    List<Movie> DeletedMovies = new List<Movie>();
    public DeletePage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }

    public async void SearchQuery(Object sender, EventArgs e)
    {

    }
    public async void deleteMovie(Object sender, EventArgs e)
    {

    }

    public async void checkDeleted(Object sender, EventArgs e)
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