using System;
using System.Net;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

public class BusinessRules : BaseConfigWP
{
    [SetUp]
    public void LocalSetup()
    {
        GetToken();
    }

    [Test]
    public void BRIdenticalComment()
    {
        // create the post to add comments to
        var requestPost = WithAuth(SetClientAndGiveRequest("posts"))
        .AddParameter ("title", "Test Businessrule" + DateTime.Now.ToString("f"))
        .AddParameter ("content", "In this test two identical comments will be posted. It is expected the first comment is allowed and the second is rejected by the businessrule")
        .AddParameter ("status", "publish")
        .AddParameter("discussion", "comments_open");
        var responsePost = ClientApi.Post(requestPost);
        var responseBodyJsonP = JObject.Parse(responsePost.Content);
        var createdId = responseBodyJsonP.SelectToken("id");
        Console.WriteLine ("Created post id: " + createdId);
        checkStatusCode(responsePost, HttpStatusCode.Created);

        // create the first comment
        var requestComment1 = WithAuth(SetClientAndGiveRequest("comments"))
        .AddParameter ("post", createdId)
        .AddParameter ("content", "Test business rule identical comment");
        var responseComment1 = ClientApi.Post(requestComment1);
        var responseBodyJsonC1 = JObject.Parse(responseComment1.Content);
        checkStatusCode(responseComment1, HttpStatusCode.Created);

        // create the second comment
        var requestComment2 = WithAuth(SetClientAndGiveRequest("comments"))
        .AddParameter ("post", createdId)
        .AddParameter ("content", "Test business rule identical comment");
        var responseComment2 = ClientApi.Post(requestComment2);
        var responseBodyJsonC2 = JObject.Parse(responseComment2.Content);
        Console.WriteLine (responseComment2);
        checkStatusCode(responseComment2, HttpStatusCode.Conflict);
    }
}