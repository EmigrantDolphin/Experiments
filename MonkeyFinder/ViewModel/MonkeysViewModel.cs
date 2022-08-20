using MonkeyFinder.Services;
using MonkeyFinder.View;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    private readonly IMonkeyService monkeyService;
    public ObservableCollection<Monkey> Monkeys { get; } = new();
    private readonly IConnectivity connectivity;
    private readonly IGeolocation geolocation;

    public MonkeysViewModel(IMonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    [ObservableProperty]
    private bool isRefreshing;

    [RelayCommand]
    private async Task GetClosestMonkey()
    {
        if (IsBusy)
            return;

        try
        {
            var location = await geolocation.GetLastKnownLocationAsync();

            if (location is null)
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromMinutes(30)
                });
            }

            if (location is null)
                return;

            var closestMonkey = Monkeys
                .OrderBy(x => Location
                    .CalculateDistance(
                        new Location(location.Latitude, location.Longitude),
                        new Location((double)x.Latitude, (double)x.Longitude),
                        DistanceUnits.Kilometers)
                ).FirstOrDefault();

            if (closestMonkey is null)
                return;

            await Shell.Current.DisplayAlert(closestMonkey.Name, closestMonkey.Location, "Ok");
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Failed to get closest monkey", "Something wrong with gps: " + ex.Message, "Ok");
        }

    }

    [RelayCommand]
    async Task GoToMonkeyDetails(Monkey monkey)
    {
        if (monkey is null)
            return;

        await Shell.Current.GoToAsync(nameof(DetailsPage), true,
            new Dictionary<string, object>
            {
                {"Monkey", monkey }
            });
    }

    [RelayCommand]
    private async Task GoToMenu()
    {
        await Shell.Current.GoToAsync(nameof(MenuPage), true);
    }

    [RelayCommand]
    async Task GetMonkeysAsync()
    {
        // var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (IsBusy)
            return;

        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No internet", "Check your internet connection and try again", "Ok");
            return;
        }

        try
        {
            IsBusy = true;

            var monkeys = await monkeyService.GetMonkeyAsync();

            if (Monkeys.Any())
            {
                Monkeys.Clear();
            }

            foreach (var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Failed to get monkeys. Message: " + ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
}
