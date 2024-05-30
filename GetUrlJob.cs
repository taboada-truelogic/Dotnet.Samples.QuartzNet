namespace Dotnet.Samples.QuartzNet;

using System.Text.Json;
using Quartz;

public class GetUrlJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using var client = new HttpClient();

        try
        {
            var uri = "https://run.mocky.io/v3/e3b44ce5-55f7-40dc-bd59-c4b9425da33d";
            Console.WriteLine($"{DateTime.Now} [Request ] {uri}");

            var response = await client.GetStringAsync(uri);
            var json = JsonDocument.Parse(response);
            var statusSummary = json.RootElement.GetProperty("statusSummary").GetString();
            Console.WriteLine($"{DateTime.Now} [Response] {statusSummary}");

            Console.Write(Environment.NewLine);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error fetching URL: {exception.Message}");
        }
    }
}
