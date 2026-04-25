namespace CritiqlyNexusCore;

public partial class UpdateSubPage : ContentPage
{
	public UpdateSubPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        LabelTitle.Text = AppData.UpdatePageSelectedMovie.title;
        LabelGenre.Text = AppData.UpdatePageSelectedMovie.genre;
        LabelPlot.Text = AppData.UpdatePageSelectedMovie.plot.Replace(".", "." + System.Environment.NewLine);
        //await DisplayAlertAsync("DEBUG", AppData.updatePageSelectedMovie.releaseDate.ToString(), "OK");
        LabelDate.Text = AppData.UpdatePageSelectedMovie.releaseDate?.ToString("yyyy-MM-dd");
        LabelPoster.Text = AppData.UpdatePageSelectedMovie.poster;
        LabelTrailer.Text = AppData.UpdatePageSelectedMovie.trailerUrl;
        LabelStreaming.Text = AppData.UpdatePageSelectedMovie.streamUrl;

        EntryTitle.Text = "";
        EntryGenre.Text = "";
        EntryPlot.Text = "";
        EntryDate.Text = "";
        EntryPoster.Text = "";
        EntryTrailer.Text = "";
        EntryStreaming.Text = "";
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
            AppData.UpdatePageSelectedMovie = null;
            await Shell.Current.GoToAsync("//UpdatePage");
        }
        else
        {
            return;
        }
    }

    public async void Save(Object sender, EventArgs e)
    {
        var updatedMovie = AppData.UpdatePageSelectedMovie;

        updatedMovie.id = AppData.UpdatePageSelectedMovie.id;
        updatedMovie.tmdb_id = AppData.UpdatePageSelectedMovie.tmdb_id;
        updatedMovie.title = string.IsNullOrWhiteSpace(EntryTitle.Text) ? AppData.UpdatePageSelectedMovie.title : EntryTitle.Text;
        updatedMovie.genre = string.IsNullOrWhiteSpace(EntryGenre.Text) ? AppData.UpdatePageSelectedMovie.genre : EntryGenre.Text;
        updatedMovie.plot = string.IsNullOrWhiteSpace(EntryPlot.Text) ? AppData.UpdatePageSelectedMovie.plot : EntryPlot.Text;
        updatedMovie.releaseDate = string.IsNullOrWhiteSpace(EntryDate.Text) ? AppData.UpdatePageSelectedMovie.releaseDate : DateTime.Parse(EntryDate.Text);
        updatedMovie.poster = string.IsNullOrWhiteSpace(EntryPoster.Text) ? AppData.UpdatePageSelectedMovie.poster : EntryPoster.Text;
        updatedMovie.trailerUrl = string.IsNullOrWhiteSpace(EntryTrailer.Text) ? AppData.UpdatePageSelectedMovie.trailerUrl : EntryTrailer.Text;
        updatedMovie.streamUrl = string.IsNullOrWhiteSpace(EntryStreaming.Text) ? AppData.UpdatePageSelectedMovie.streamUrl : EntryStreaming.Text;
        updatedMovie.IsUpdated = true;

        await Shell.Current.GoToAsync("//UpdatePage");
    }
}