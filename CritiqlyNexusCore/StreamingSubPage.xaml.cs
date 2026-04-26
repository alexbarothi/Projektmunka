namespace CritiqlyNexusCore;
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