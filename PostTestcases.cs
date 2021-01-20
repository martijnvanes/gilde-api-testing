using System;
using System.Net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

public class ExampleTestsWithAuthorization
{
    private string _apiKey;
    [SetUp]
    public void Setup()
    {
        var client = new RestClient ("http://acc.polteq-testing.com:8000/wp-json/jwt-auth/v1/token");
        var request = new RestRequest()
        .AddParameter("username","Trainee5@polteq.com")
        .AddParameter("password", "1AmAPolteqTrainee5");
        var response = client.Post(request);
        var responseBodyJson = JObject.Parse(response.Content);
        _apiKey="Bearer" +(string)responseBodyJson.SelectToken("token");        
    }

    [Test]
    public void ExampleFirstPost()
    {
        var client=new RestClient("http://acc.polteq-testing.com:8000/wp-json/wp/v2");
        var request = new RestRequest("comments")
        .AddHeader("Authorization", _apiKey)
        .AddParameter("post", "1")
        .AddParameter("content", "RestSharp Martijn" + DateTime.Now.ToString("f"));
        var response = client.Post(request);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "StatusCode not correct");
    }
}