using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.IO;
namespace Gorsel_Final;

public partial class Yapilacaklar : ContentPage
{
	public Yapilacaklar()
	{
		InitializeComponent();
		todos.ItemsSource = Missions;
		if (File.Exists(dosyaadı)) 
		{
			string data = File.ReadAllText(dosyaadı);
			missions= JsonSerializer.Deserialize<ObservableCollection<Missions>>(data);

		}
	}

	  string dosyaadı = Path.Combine(FileSystem.AppDataDirectory, "data.json");

	public ObservableCollection<Missions> Missions => missions;


	
	 ObservableCollection<Missions> missions = new () 
	{
			
		
	};

	static FirebaseClient client = new FirebaseClient("https://gorselodev-8822b-default-rtdb.firebaseio.com/");//
	private async void YeniGorev(object sender, EventArgs e)
	{
		
		Planlama planlama=new Planlama() { Title="Title",AddMethod=EkleGorev};
		await Navigation.PushModalAsync(planlama);
	}
	
	private async void GorevGuncelle(object sender,EventArgs e)
	{
		var Duzernle =(ImageButton)sender;
		if (Duzernle != null) 
		{
			await client.Child("Yapilacaklar").PutAsync(missions);
			var id=Duzernle.CommandParameter.ToString();
			var sondurum = Missions.Single(o => o.ID ==id);

			Planlama planlama = new Planlama() { Title = "Title", Yaplacaklar=sondurum };
			await Navigation.PushModalAsync(planlama);
		}
		
		
	}
	public async void EkleGorev(Missions mission) 
	{
		await client.Child("Yapilacaklar").PostAsync(mission);
		missions.Add(mission);
		
	}
	private async void Delete(object sender, EventArgs e)
	{
        ImageButton button = (ImageButton)sender;
		var sil = await DisplayAlert("Sil", "Emin Misiniz?", "Evet", "Hayır");
		if (sil)
		{
			
			var temzile = button.BindingContext as Missions;
			if (temzile != null)
			{
				
				missions.Remove(temzile);
				await client.Child("Yapilacaklar").DeleteAsync();


			}
		}
	}


}
public class Missions:INotifyPropertyChanged
{
	
	public string ID
	{
		get
		{
			if (id == null)
				id = Guid.NewGuid().ToString();

			return id;
		}
		set { id = value; }
	}
	public string id, head, detail, date, hour;
	public string Head { get=>head; set { head = value; NotifyPropertyChanged(); } }
	public string Detail { get=>detail; set { detail = value; NotifyPropertyChanged(); } }
	public string Date { get=>date; set { date = value; NotifyPropertyChanged(); } }
	public string Hour { get=>hour; set { hour = value; NotifyPropertyChanged(); } }


	public event PropertyChangedEventHandler PropertyChanged;

	public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	}
	
}
