using Firebase.Database;
using Firebase.Database.Query;
using System.Reflection;
using static Gorsel_Final.Yapilacaklar;


namespace Gorsel_Final;

public partial class Planlama : ContentPage
{
	public Planlama()
	{
		InitializeComponent();
	}
    private void GoBack_Clicked(object sender, System.EventArgs e)
    {
        Navigation.PopModalAsync();
    }


    public bool result { get; set; } = false;
	public   Missions Yaplacaklar;
	public  Action<Missions> AddMethod { get; internal set; }
	
	private   void Save(object sender, EventArgs e) 
	{
		
		if (Yaplacaklar == null) 
		{ 
			Yaplacaklar = new Missions()
			{

                Head = Bas.Text,
				Detail=Detay.Text,
				Date=Tarih.Date.ToShortDateString(),
				Hour=Saat.Time.ToString(),
			
			};		
		}
		else
		{
			Yaplacaklar.Head = Bas.Text;
			Yaplacaklar.Detail=Detay.Text;
			Yaplacaklar.Date = Tarih.Date.ToShortDateString();
			Yaplacaklar.Hour = Saat.Time.ToString();
		}

		if (AddMethod != null)
			AddMethod(Yaplacaklar);
			


		Navigation.PopModalAsync();
	}
	private async void Cancel(object sender, EventArgs e) 
	{
		await Navigation.PopModalAsync();
	}
}