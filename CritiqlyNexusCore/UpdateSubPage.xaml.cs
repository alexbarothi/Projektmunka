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

        EntryTitle.Text = "";
        EntryGenre.Text = "";
        EntryPlot.Text = "";
        EntryDate.Text = "";
        EntryPoster.Text = "";
    }
}