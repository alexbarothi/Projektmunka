using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using CritiqlyNexusCore.Models;

namespace CritiqlyNexusCore;

public partial class StreamingPage : ContentPage
{
    public ObservableCollection<Movie> QueryMovies { get; set; } = new ObservableCollection<Movie>();
    public List<StreamingVote> verifiedStreaming = new List<StreamingVote>();
    public StreamingPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        StatusLabel.Text = "Válassz ki egy hitelesítésre váró filmet!";

        CheckQueue(this, EventArgs.Empty);

    }
    public async void VerifyMovie(Object sender, EventArgs e)
    {
        int selectedId = 0;
        var Button = sender as Button;
        var id = Button?.CommandParameter;

        AppData.StreamingPageSelectedToVerify = AppData.streamingVotes.First(x => x.MovieId == (Int32)id);
        AppData.StreamingPageSelectedMovie = AppData.Movies.First(x => x.id == (Int32)id);

        await Shell.Current.GoToAsync("//StreamingSubPage");
    }

    public async void CheckQueue(Object sender, EventArgs e)
    {
        QueryMovies.Clear();

        List<int> tempIdList = new List<int>();

        foreach (StreamingVote data in AppData.streamingVotes)
        {
            if (data.Netflix >= 30 || data.Hbo >= 30 || data.Amazon >= 30 || data.Disney >= 30 || data.Apple >= 30)
            {
                tempIdList.Add(data.MovieId);
            }
        }

        foreach (Movie movie in AppData.Movies)
        {
            if (tempIdList.Contains(movie.id))
            {
                QueryMovies.Add(movie);
            }
        }

        tempIdList.Clear();
    }

    public async void CheckVerified(Object sender, EventArgs e)
    {
        QueryMovies.Clear();

        List<int> tempIdList = new List<int>();

        foreach (StreamingVote data in verifiedStreaming)
        {
            tempIdList.Add(data.MovieId);
        }

        foreach (Movie movie in AppData.Movies)
        {
            if (tempIdList.Contains(movie.id))
            {
                QueryMovies.Add(movie);
            }
        }
    }

    public async void Exit(Object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}