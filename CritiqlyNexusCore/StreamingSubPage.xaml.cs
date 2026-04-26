namespace CritiqlyNexusCore;

using System.Text;
using System.Text.Json;
using CritiqlyNexusCore.Models;

public partial class StreamingSubPage : ContentPage
{
    public StreamingSubPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        LoadSelectedMovie();
    }

    public void LoadSelectedMovie()
    {
        MovieTitleLabel.Text = AppData.StreamingPageSelectedMovie.title;
        MoviePosterImage.Source = AppData.StreamingPageSelectedMovie.fullPosterUrl;

        NetflixVotesLabel.Text = "Netflix - " + AppData.StreamingPageSelectedToVerify.Netflix + " Szavazat";
        HboVotesLabel.Text = "HBO MAX - " + AppData.StreamingPageSelectedToVerify.Hbo + " Szavazat";
        AmazonVotesLabel.Text = "Amazon Prime - " + AppData.StreamingPageSelectedToVerify.Amazon + " Szavazat";
        AppleVotesLabel.Text = "Apple TV+ - " + AppData.StreamingPageSelectedToVerify.Apple + " Szavazat";
        DisneyVotesLabel.Text = "Disney+ - " + AppData.StreamingPageSelectedToVerify.Disney + " Szavazat";
    }

    public async void Save(object sender, EventArgs e)
    {
        var client = new HttpClient();

        AppData.StreamingPageSelectedMovie.streamUrl = StreamingLinkEntry.Text;

        if (NetflixRadioButton.IsChecked)
        {
            AppData.StreamingPageSelectedToVerify.VerifiedPlatform = "netflix";
        }
        else if (HboRadioButton.IsChecked)
        {
            AppData.StreamingPageSelectedToVerify.VerifiedPlatform = "hbo";
        }
        else if (AmazonRadioButton.IsChecked)
        {
            AppData.StreamingPageSelectedToVerify.VerifiedPlatform = "amazon";
        }
        else if (AppleRadioButton.IsChecked)
        {
            AppData.StreamingPageSelectedToVerify.VerifiedPlatform = "apple";
        }
        else if (DisneyRadioButton.IsChecked)
        {
            AppData.StreamingPageSelectedToVerify.VerifiedPlatform = "disney";
        }

        var verifyData = new
        {
            movie_id = AppData.StreamingPageSelectedToVerify.MovieId,
            netflix = AppData.StreamingPageSelectedToVerify.Netflix,
            disney = AppData.StreamingPageSelectedToVerify.Disney,
            hbo = AppData.StreamingPageSelectedToVerify.Hbo,
            apple = AppData.StreamingPageSelectedToVerify.Apple,
            amazon = AppData.StreamingPageSelectedToVerify.Amazon,
            verified_platform = AppData.StreamingPageSelectedToVerify.VerifiedPlatform
        };

        var jsonVerify = JsonSerializer.Serialize(verifyData);
        var httpVerifyData = new StringContent(jsonVerify, Encoding.UTF8, "application/json");

        var responseV = await client.PostAsync($"http://localhost:8000/api/verify-votes/{AppData.StreamingPageSelectedToVerify.MovieId}", httpVerifyData);

        var movieData = new
        {
            tmdb_id = AppData.StreamingPageSelectedMovie.tmdb_id,
            title = AppData.StreamingPageSelectedMovie.title,
            genre = AppData.StreamingPageSelectedMovie.genre,
            plot = AppData.StreamingPageSelectedMovie.plot,
            releaseDate = AppData.StreamingPageSelectedMovie.releaseDate?.ToString("yyyy-MM-dd"),
            poster = AppData.StreamingPageSelectedMovie.poster,
            trailerUrl = AppData.StreamingPageSelectedMovie.trailerUrl,
            streamUrl = AppData.StreamingPageSelectedMovie.streamUrl,
            deleted_at = (DateTime?)null
        };

        var jsonMovie = JsonSerializer.Serialize(movieData);
        var httpMovieData = new StringContent(jsonMovie, Encoding.UTF8, "application/json");

        var responseM = await client.PutAsync($"http://localhost:8000/api/movies/{AppData.StreamingPageSelectedMovie.id}", httpMovieData);

        if(!responseM.IsSuccessStatusCode)
        {
            await DisplayAlertAsync("Hiba", "A mentés nem sikerült! \n" +
            "Próbáld újra!", "OK");
        }

        if (!responseV.IsSuccessStatusCode)
        {
            await DisplayAlertAsync("Hiba", "A mentés nem sikerült! \n" +
            "Próbáld újra!", "OK");
        }

        if(responseV.IsSuccessStatusCode && responseM.IsSuccessStatusCode)
        {
            Shell.Current.GoToAsync("//StreamingPage");
        }
    }

    public async void Back(object sender, EventArgs e)
    {
        var isResponseOk = await DisplayAlertAsync(
            "Kilépés",
            "Biztosan ki akarsz lépni?",
            "Igen",
            "Mégse"
        );

        if (isResponseOk)
        {
            await Shell.Current.GoToAsync("//StreamingPage");
        }
        else
        {
            return;
        }
    }
}