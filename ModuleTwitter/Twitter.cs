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
        public ModuleTwitter()
        {
            Auth.SetUserCredentials("hAVBTJykgQyF6bkxsSNmTs7mj", "dnr2QSlGlq5dyiecgZqLDBdtqYpfXN7a5MCwH9AkgYAozgrBJ6", "922446818033664001-wwrq7uhrWDJGdrWONt9W1n9208KrSER", "4Yx5KgWPmgpzWQ2AEzN58bykrmPiMrZr9TSoYuKSH28hP");
            var stream = Stream.CreateUserStream();
            stream.TweetCreatedByMe += (sender, args) =>
            {
                Console.WriteLine(args.Tweet);
            };
            stream.StartStreamAsync();
        }

        public override string GetRequest()
        {
            var user = User.GetAuthenticatedUser();
            //Console.WriteLine("bamboula");
            //var tweet = Tweet.PublishTweet("BRING IT ON BIATCH !!");
            return JsonConvert.SerializeObject(user.ScreenName, new JsonApiSerializerSettings());
        }

        public override string PostRequest()
        {
            var tweet = Tweet.PublishTweet("BRING IT ON BIATCH !!");
            return JsonConvert.SerializeObject("", new JsonApiSerializerSettings());
        }
    }
}