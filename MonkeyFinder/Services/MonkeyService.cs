using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public interface IMonkeyService
{
    public Task<List<Monkey>> GetMonkeyAsync();
}

public class MonkeyService : IMonkeyService
{
    private const string monkeyUrl = "https://montemagno.com/monkeys.json";
    private readonly HttpClient httpClient;
    private List<Monkey> monkeyList = new();

    public MonkeyService()
    {
        httpClient = new HttpClient();
    }

    public async Task<List<Monkey>> GetMonkeyAsync()
    {
        if (monkeyList.Any())
        {
            return monkeyList;
        }

        var response = await httpClient.GetAsync(monkeyUrl);

        if (response.IsSuccessStatusCode)
        {
            monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }

        return monkeyList;
    }
}
