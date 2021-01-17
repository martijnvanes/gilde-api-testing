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
        }
    }
}