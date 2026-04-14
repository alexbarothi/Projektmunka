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
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        EntryQuery.Text = "";
        StatusLabel.Text = "Kérlek válaszd ki a törölni kívánt filmet!";

        QueryMovies.Clear();

        SearchQuery(this, EventArgs.Empty);
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
    public async void deleteMovie(Object sender, EventArgs e)
    {
        var Button = sender as Button;
        var id = Button?.CommandParameter;

        if (DeletedMovies.Contains(AppData.Movies.First(x => x.id == (Int32)id)))
        {
            Movie sel = DeletedMovies.First(x => x.id == (Int32)id);
            DeletedMovies.Remove(sel);
            sel.IsDeleted = false;
            checkDeleted(this, EventArgs.Empty);
        }
        else
        {
            Button.BackgroundColor = Colors.Orange;
            Movie del = AppData.Movies.First(x => x.id == (Int32)id);
            del.IsDeleted = true;
            DeletedMovies.Add(del);
            await Task.Delay(500);
            Button.BackgroundColor = Color.FromRgb(212, 255, 62);
            SearchQuery(this, EventArgs.Empty);
        }
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