using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;

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

            var deserialize = new JsonDeserializer();
            var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            Console.WriteLine ("Deserialized output");
            var result = output["au"]
            Console.WriteLine(output);
        }
    }
}