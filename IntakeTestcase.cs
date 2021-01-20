using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;

namespace api_testing

{
    public class IntakeTestcase
    {
        [Test]
        public void MyFirstGetTest()
        {
            var client = new RestClient("http://acc.polteq-testing.com:8000/wp-json/wp/v2");
            var request = new RestRequest("posts/13");
            var response = client.Get(request);
            Console.WriteLine(response.Content);

            // var deserialize = new JsonDeserializer();
            // var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            // Console.WriteLine ("Deserialized output");
            // var result = output["id"];
            JObject obs = JObject.Parse(response.Content);
            // Console.WriteLine(obs);
            Assert.That(obs["id"].ToString(), Is.EqualTo("13"), "Id is not correct");
        }

        [Test]
        public void CreateNewPostWithAnonimousBody()
        {
            var client = new RestClient("https://public-api.wordpress.com/rest/v1.1/sites/113768211");
            var request = new RestRequest("posts/new", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { title = "Post van Martijn met REST Sharp"});
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}