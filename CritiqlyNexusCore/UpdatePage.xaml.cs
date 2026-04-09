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

    }
    public async void EditMovie(Object sender, EventArgs e)
    {

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