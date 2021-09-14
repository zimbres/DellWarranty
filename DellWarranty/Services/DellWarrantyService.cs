using RestSharp;

namespace DellWarranty.Services;
public class DellWarrantyService
{
    private readonly IConfiguration _configuration;

    public DellWarrantyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<DellWarrantyPayload>> GetDellWarranty(string serviceTags)
    {
        var apiSettings = _configuration.GetSection("ApiSettings").Get<ApiSettings>();

        var auth = await GetDellOAuth(apiSettings);

		var client = new RestClient($"{apiSettings.EndpointUrl}{serviceTags}")
		{
			Timeout = 60000
		};
		var request = new RestRequest(Method.GET);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {auth.AccessToken}");
        var response = await client.GetAsync<List<DellWarrantyPayload>>(request);
        return response;
    }

    private static async Task<DellOAuthPayload> GetDellOAuth(ApiSettings apiSettings)
    {
        var client = new RestClient(apiSettings.AuthUrl)
		{
			Timeout = 60000
        };
		var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("client_id", $"{apiSettings.ClientId}");
        request.AddParameter("client_secret", $"{apiSettings.ClientSecret}");
        request.AddParameter("grant_type", "client_credentials");
        var response = await client.PostAsync<DellOAuthPayload>(request);
        return response;
    }
}
