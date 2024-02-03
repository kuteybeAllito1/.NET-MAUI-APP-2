using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;

namespace Gorsel_Final;
public partial class Sehir_Hava_Durumu : ContentPage
{

    public Sehir_Hava_Durumu()
    {
        InitializeComponent();
        if (File.Exists(fileName))
        {
            string data = File.ReadAllText(fileName);
            Sehirler = System.Text.Json.JsonSerializer.Deserialize<ObservableCollection<HavaDurum1>>(data);
        }
        listCity.ItemsSource = Sehirler;

    }
    string fileName = Path.Combine(FileSystem.Current.AppDataDirectory, "hdata.json");
    ObservableCollection<HavaDurum1> Sehirler = new ObservableCollection<HavaDurum1>();
    private async void EkleClicked(object sender, EventArgs e)
    {
        string sehir = await DisplayPromptAsync("Şehir:", "Şehir ismi", "OK", "Cancel");
        if (sehir != null)
        {
            sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
            sehir = sehir.Replace('Ç', 'C');
            sehir = sehir.Replace('Ð', 'G');
            sehir = sehir.Replace('Ý', 'I');
            sehir = sehir.Replace('Ö', 'O');
            sehir = sehir.Replace('Ü', 'U');
            sehir = sehir.Replace('Þ', 'S');
            Sehirler.Add(new HavaDurum1() { Name = sehir });

            string data = System.Text.Json.JsonSerializer.Serialize(Sehirler);
        }

    }
    private void YukleClicked(object sender, EventArgs e)
    {
        refreshView.IsRefreshing = true;
    }
    private async void silclicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;

        if (button != null)
        {
            var info = Sehirler.First(o => o.Name == button.CommandParameter.ToString());
            var control = await DisplayAlert("Silinsin mi ?", "Silmeyi onayla", "Tamam", "Iptal");

            if (control)
            {
                Sehirler.Remove(info);
                string data = System.Text.Json.JsonSerializer.Serialize(Sehirler);
                File.WriteAllText(fileName, data);
            }
        }
    }
}
public  class HavaDurum1
{
    public string Name { get; set; }

    public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";
}







