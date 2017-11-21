using JsonApiSerializer;
using Newtonsoft.Json;
using Tweetinvi;
using System;
using System.Security.Cryptography;

namespace Module
{
    public class ModuleTwitter : AModule
    {
        private TwitterStreaming Ts { get; set; }
        
        public ModuleTwitter()
        {
            Auth.SetUserCredentials("hAVBTJykgQyF6bkxsSNmTs7mj", "dnr2QSlGlq5dyiecgZqLDBdtqYpfXN7a5MCwH9AkgYAozgrBJ6", "922446818033664001-wwrq7uhrWDJGdrWONt9W1n9208KrSER", "4Yx5KgWPmgpzWQ2AEzN58bykrmPiMrZr9TSoYuKSH28hP");
            //ts = new TwitterStreaming();
        }

        /**
         * Actions
         */
        public string GetUsername()
        {
            var user = User.GetAuthenticatedUser();
            if (user.ScreenName.Equals(""))
                return ("Error");
            return JsonConvert.SerializeObject(user.ScreenName, new JsonApiSerializerSettings());
        }

        public string PostRequest(string msg)
        {
            if (msg.Equals(""))
                return ("Error");
            var tweet = Tweet.PublishTweet(msg);
            return JsonConvert.SerializeObject(msg, new JsonApiSerializerSettings());
        }
        
        /**
         * Reactions
         */
        public ReactionResult ReactionTweet(DAO.User user, string msg)
        {
            var result = new ReactionResult();
            return result;
        }
    }
}