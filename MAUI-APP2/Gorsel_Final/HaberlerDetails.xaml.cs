namespace Gorsel_Final;

public partial class haberlerDetails : ContentPage
{
    Item haber;

    

    public haberlerDetails(Item item)
    {
        InitializeComponent();
        haber = item;
        LoadWebView();
    }

    private async void OnBackToNewsClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    private void LoadWebView()
    {
       
        if (!string.IsNullOrEmpty(haber?.link))
        {
            webView.Source = new Uri(haber.link);
        }
        else
        {
           
            webView.Source = "about:blank";
        }
    }

    private async void Paylas(object sender, EventArgs e)
    {
        await shareUri(haber.link, Share.Default);
    }

    private async Task shareUri(string link, IShare share)
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Uri = link
        });
        Title = haber.title;
    }

    private async void Geri(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
