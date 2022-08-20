namespace MonkeyFinder.ViewModel;

[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
	private readonly IMap map;

	public MonkeyDetailsViewModel(IMap map)
	{
		this.map = map;
	}

	[ObservableProperty]
	Monkey monkey;

	[RelayCommand]
	private async Task OpenMap()
	{
		try
		{
			await map.OpenAsync(new Location((double)Monkey.Latitude, (double)Monkey.Longitude),
				new MapLaunchOptions
				{
					Name = Monkey.Name,
					NavigationMode = NavigationMode.None
				});
		}
		catch(Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", "Can't open map: " + ex.Message, "Ok");
		}
	}
}
