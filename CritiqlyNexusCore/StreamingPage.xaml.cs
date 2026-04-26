using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using CritiqlyNexusCore.Models;

namespace CritiqlyNexusCore;

public partial class StreamingPage : ContentPage
{
    public StreamingPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        StatusLabel.Text = "Válassz ki egy hitelesítésre váró filmet!";

    }
    public async void VerifyMovie(Object sender, EventArgs e)
    {

    }

    public async void CheckQueue(Object sender, EventArgs e)
    {

    }

    public async void CheckVerified(Object sender, EventArgs e)
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