using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using CritiqlyNexusCore.Models;

namespace CritiqlyNexusCore;

public partial class UpdatePage : ContentPage
{
    public ObservableCollection<Movie> QueryMovies { get; set; } = new ObservableCollection<Movie>();
    public int selectedId;
    List<Movie> UpdatedMovies = new List<Movie>();
    public UpdatePage()
	{
		InitializeComponent();
        BindingContext = this;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        EntryQuery.Text = "";
        StatusLabel.Text = "Kérlek válaszd ki a szerkeszteni kívánt filmet!";

        if (AppData.UpdatePageSelectedMovie != null)
        {
            UpdatedMovies.Add(AppData.UpdatePageSelectedMovie);
        }

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
    public async void EditMovie(Object sender, EventArgs e)
    {
        selectedId = 0;
        var Button = sender as Button;
        var id = Button?.CommandParameter;
        AppData.UpdatePageSelectedMovie = AppData.Movies.First(x => x.id == (Int32)id);

        if (UpdatedMovies.Contains(AppData.Movies.First(x => x.id == (Int32)id)))
        {
            Movie sel = UpdatedMovies.First(x => x.id == (Int32)id);
            UpdatedMovies.Remove(sel);
            sel.IsUpdated = false;
            checkSelected(this, EventArgs.Empty);
        }
        else
        {
            Button.BackgroundColor = Colors.Orange;
            await Task.Delay(500);
            await Shell.Current.GoToAsync("//UpdateSubPage");
            Button.BackgroundColor = Color.FromRgb(212, 255, 62);
        }
    }

    public async void checkSelected(Object sender, EventArgs e)
    {

    }

    public async void Exit(Object sender, EventArgs e)
    {

    }

    public async void Save(Object sender, EventArgs e)
    {

    }
}