//using LiteDB;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
namespace Gorsel_Final;

public partial class haber_liste : ContentPage
{
	public haber_liste()
	{
		InitializeComponent();
        category.ItemsSource = Kategori.liste;
        _ = Load();
    }

    Root root;
    Kategori glb;

    private async void OnImageTapped(object sender, EventArgs e)
    {
        var selectedItem = (sender as Image)?.BindingContext as Item;

        if (selectedItem != null)
        {
            haberlerDetails newsDetail = new haberlerDetails(selectedItem);
            await Navigation.PushModalAsync(newsDetail);
        }
    }

    async Task Load()
    {
        string jsondata = await HaberleriGetir(glb);
        root = System.Text.Json.JsonSerializer.Deserialize<Root>(jsondata);
        lsHaberler.ItemsSource = root.items;
    }

    private async void category_Select(object sender, SelectionChangedEventArgs e)
    {

        var label = (e.CurrentSelection.FirstOrDefault() as Kategori)?.Basliklar;



        if (label != null)
        {
            foreach (var category in Kategori.liste)
            {
                if (category.Basliklar.Equals(label))
                {
                    glb = category;

                    break;
                }
            }

        }


        await Load();



    }

    private void lsHaberler_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        Item selectedItem = e.CurrentSelection.FirstOrDefault() as Item;

        if (selectedItem != null)
        {
            haberlerDetails newsDetail = new haberlerDetails(selectedItem);
            Navigation.PushModalAsync(newsDetail);
        }

    }

    public static async Task<string> HaberleriGetir(Kategori ctg)
    {
        if (ctg == null)
        {
            HttpClient client = new HttpClient();
            string url = "https://api.rss2json.com/v1/api.json?rss_url=https://www.trthaber.com/ekonomi_articles.rss";
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsondata = await response.Content.ReadAsStringAsync();
            return jsondata;
        }
        else
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = $"https://api.rss2json.com/v1/api.json?rss_url={ctg.Link}";
                using HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsondata = await response.Content.ReadAsStringAsync();
                return jsondata;
            }
            catch
            {
                return null;
            }
        }

    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        _ = Load();
    }
}

public class Kategori
{
    public string Basliklar { get; set; }
    public string Link { get; set; }
    public static List<Kategori> liste = new List<Kategori>()
 {
 new Kategori() { Basliklar = "Manşet", Link = "https://www.trthaber.com/manset_articles.rss"},
 new Kategori() { Basliklar = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss"},
 new Kategori() { Basliklar = "Gündem", Link = "https://www.trthaber.com/gundem_articles.rss"},
 new Kategori() { Basliklar = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss"},
 new Kategori() { Basliklar = "Spor", Link = "https://www.trthaber.com/spor_articles.rss"},
 new Kategori() { Basliklar = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss"},
 new Kategori() { Basliklar = "Güncel", Link = "https://www.trthaber.com/guncel_articles.rss"},
 new Kategori() { Basliklar = "Eğitim", Link = "https://www.trthaber.com/egitim_articles.rss"},
 };
}


public class Enclosure
{
    public string link { get; set; }
    public string type { get; set; }
}

public class Feed
{
    public string url { get; set; }
    public string title { get; set; }
    public string link { get; set; }
    public string author { get; set; }
    public string description { get; set; }
    public string image { get; set; }
}

public class Item
{
    public string title { get; set; }
    public string pubDate { get; set; }
    public string link { get; set; }
    public string guid { get; set; }
    public string author { get; set; }
    public string thumbnail { get; set; }
    public string description { get; set; }
    public string content { get; set; }
    public Enclosure enclosure { get; set; }
    public List<object> categories { get; set; }
}

public class Root
{
    public string status { get; set; }
    public Feed feed { get; set; }
    public List<Item> items { get; set; }
}
