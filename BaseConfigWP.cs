using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

public class BaseConfigWP
{
    private const string BaseUrl = "http://acc.polteq-testing.com:8000/";
    private const string AuthPath = "/wp-json/jwt-auth/v1/token";
    private const string ApiPath = "wp-json/wp/v2";

    private string _apiKey;
    protected RestClient ClientApi;
    protected void GetToken()
    {
        var client = new RestClient(BaseUrl);
        var request = new RestRequest(AuthPath)
        .AddParameter("username", "Trainee5")
        .AddParameter("password", "1AmAPolteqTrainee5");
        var response = client.Post(request);
        var responseBodyJson=JObject.Parse(response.Content);
        _apiKey = "Bearer" + (string)responseBodyJson.SelectToken("token");
    }
    protected RestRequest SetClientAndGiveRequest(string resource)
    {
        ClientApi = new RestClient(BaseUrl + ApiPath);
        return new RestRequest(resource);
    }
    protected IRestRequest WithAuth(IRestRequest restRequest)
    {
        return restRequest.AddHeader("Authorization", _apiKey);
    }
    protected void checkStatusCode(IRestResponse response, System.Net.HttpStatusCode statusCode)
    {
        Assert.AreEqual(statusCode, response.StatusCode, "Status code not correct");
    }
}