using System.Collections.Generic;
using JsonApiSerializer;
using Module;
using Newtonsoft.Json;
using Tweetinvi;
using System;

namespace Module
{
    public class ModuleTwitter : AModule
    {
        private static TwitterStreaming ts;
        public ModuleTwitter()
        {
            Auth.SetUserCredentials("hAVBTJykgQyF6bkxsSNmTs7mj", "dnr2QSlGlq5dyiecgZqLDBdtqYpfXN7a5MCwH9AkgYAozgrBJ6", "922446818033664001-wwrq7uhrWDJGdrWONt9W1n9208KrSER", "4Yx5KgWPmgpzWQ2AEzN58bykrmPiMrZr9TSoYuKSH28hP");
            ts = new TwitterStreaming();
        }

        public override string GetRequest()
        {
            var user = User.GetAuthenticatedUser();
            return JsonConvert.SerializeObject(user.ScreenName, new JsonApiSerializerSettings());
        }

        public override string PostRequest()
        {
            return JsonConvert.SerializeObject("test", new JsonApiSerializerSettings());
        }

        public string PostRequest(string msg)
        {
            if (msg == "" || msg == null)
                return JsonConvert.SerializeObject("need params after post request", new JsonApiSerializerSettings());
            var tweet = Tweet.PublishTweet(msg);
            return JsonConvert.SerializeObject(msg, new JsonApiSerializerSettings());
        }
    }
}