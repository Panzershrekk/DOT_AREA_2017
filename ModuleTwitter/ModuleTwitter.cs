using Tweetinvi;
using System;

namespace Module
{
    public class ModuleTwitter : AModule
    {
        public ModuleTwitter()
        {
            Auth.SetUserCredentials("hAVBTJykgQyF6bkxsSNmTs7mj",
                "dnr2QSlGlq5dyiecgZqLDBdtqYpfXN7a5MCwH9AkgYAozgrBJ6",
                "922446818033664001-wwrq7uhrWDJGdrWONt9W1n9208KrSER",
                "4Yx5KgWPmgpzWQ2AEzN58bykrmPiMrZr9TSoYuKSH28hP");
        }

        /**
         * Actions
         */
        public string TwitterGetUsername()
        {
            var user = User.GetAuthenticatedUser();
            return user.ScreenName;
        }

        public bool TwitterPostRequest(string msg)
        {
            if (msg.Equals(""))
                return false;
            Tweet.PublishTweet(msg);
            return true;
        }

        /**
         * Reactions
         */
        public ReactionResult ReactionTweet(DAO.User user, string msg)
        {
            var result = new ReactionResult();
            try
            {
                var tweet = Tweet.PublishTweet(msg);
                result.Type = ReactionStatus.Ok;
            }
            catch (Exception e)
            {
                result.Type = ReactionStatus.Error;
                result.Message = "Cannot publish to twitter";
            }
            return result;
        }

    }
}