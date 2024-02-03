using System.Text.Json;

namespace Gorsel_Final;

public partial class Doviz_Kurlar : ContentPage
{
    public Doviz_Kurlar()
    {
        InitializeComponent();
    }

    private static Doviz_Kurlar instance;
    public static Doviz_Kurlar Page
    {
        get
        {
            if (instance == null)
                instance = new Doviz_Kurlar();
            return instance;
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await Load();
    }

    AltinDoviz kurlar;

    async Task Load()
    {
        string jsondata = await GetAltinDovizGuncelKurlar();

        kurlar = JsonSerializer.Deserialize<AltinDoviz>(jsondata);

        dovizliste.ItemsSource = new List<Doviz>()
        {
            new Doviz()
            {
                Name = "Dolar",
                AlisFiat = kurlar.USD.alis,
                SatisFiat = kurlar.USD.satis,
                Farklar = kurlar.USD.degisim,
                Yon = GetImage(kurlar.USD.d_yon),
            },

            new Doviz()
            {
                Name = "Euro",
                AlisFiat = kurlar.EUR.alis,
                SatisFiat = kurlar.EUR.satis,
                Farklar = kurlar.EUR.degisim,
                Yon = GetImage(kurlar.EUR.d_yon),
            },

            new Doviz()
            {
                Name = "Sterlin",
                AlisFiat = kurlar.GBP.alis,
                SatisFiat = kurlar.GBP.satis,
                Farklar = kurlar.GBP.degisim,
                Yon = GetImage(kurlar.GBP.d_yon),
            },

            new Doviz()
            {
                Name = "",
                AlisFiat = "",
                SatisFiat = "",
                Farklar = "",
                Yon = "",
            },


            new Doviz()
            {
                Name = "Gram Altin",
                AlisFiat = kurlar.GA.alis,
                SatisFiat = kurlar.GA.satis,
                Farklar = kurlar.GA.degisim,
                Yon = GetImage(kurlar.GA.d_yon),
            },

            new Doviz()
            {
                Name = "Çeyrek Altin",
                AlisFiat = kurlar.C.alis,
                SatisFiat = kurlar.C.satis,
                Farklar = kurlar.C.degisim,
                Yon = GetImage(kurlar.C.d_yon),
            },

            new Doviz()
            {
                Name = "Gümüş",
                AlisFiat = kurlar.GAG.alis,
                SatisFiat = kurlar.GAG.satis,
                Farklar = kurlar.GAG.degisim,
                Yon = GetImage(kurlar.GAG.d_yon),
            },

            new Doviz()
            {
                Name = "",
                AlisFiat = "",
                SatisFiat = "",
                Farklar = "",
                Yon = "",
            },


            new Doviz()
            {
                Name = "BTC",
                AlisFiat = kurlar.BTC.alis,
                SatisFiat = kurlar.BTC.satis,
                Farklar = kurlar.BTC.degisim,
                Yon = GetImage(kurlar.BTC.d_yon),
            },

            new Doviz()
            {
                Name = "ETH",
                AlisFiat = kurlar.ETH.alis,
                SatisFiat = kurlar.ETH.satis,
                Farklar = kurlar.ETH.degisim,
                Yon = GetImage(kurlar.ETH.d_yon),
            },

            new Doviz()
            {
                Name = "",
                AlisFiat = "",
                SatisFiat = "",
                Farklar = "",
                Yon = "",
            },

            new Doviz()
            {
                Name = "BIST100",
                AlisFiat = kurlar.XU100.alis,
                SatisFiat = kurlar.XU100.satis,
                Farklar = kurlar.XU100.degisim,
                Yon = GetImage(kurlar.ETH.d_yon),
            },

        };
    }

    private string GetImage(string str)
    {
        if (str.Contains("up"))
            return "up.png";
        if (str.Contains("down"))
            return "down.png";
        

        return "";
    }

    async Task<string> GetAltinDovizGuncelKurlar()
    {
        string url = "https://api.genelpara.com/embed/altin.json";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string jsondata = await response.Content.ReadAsStringAsync();
        return jsondata;
    }

    private async void Guncelleme(object sender, EventArgs e)
    {
        await Load();
    }
}

public class Doviz
{
    public string Name { get; set; }
    public string AlisFiat { get; set; }
    public string SatisFiat { get; set; }
    public string Farklar { get; set; }
    public string Yon { get; set; }
}

public class AltinDoviz
{
    public USD USD { get; set; }
    public EUR EUR { get; set; }
    public GBP GBP { get; set; }
    public GA GA { get; set; }
    public C C { get; set; }
    public GAG GAG { get; set; }
    public BTC BTC { get; set; }
    public ETH ETH { get; set; }
    public XU100 XU100 { get; set; }
}

public class BTC
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class C
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class ETH
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class EUR
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class GA
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class GAG
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class GBP
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}



public class USD
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class XU100
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
}
