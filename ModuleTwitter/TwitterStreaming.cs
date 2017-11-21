using Tweetinvi;
using System;

namespace Module
{
    public class TwitterStreaming
    {

        public TwitterStreaming()
        {
            Auth.SetUserCredentials("hAVBTJykgQyF6bkxsSNmTs7mj", "dnr2QSlGlq5dyiecgZqLDBdtqYpfXN7a5MCwH9AkgYAozgrBJ6", "922446818033664001-wwrq7uhrWDJGdrWONt9W1n9208KrSER", "4Yx5KgWPmgpzWQ2AEzN58bykrmPiMrZr9TSoYuKSH28hP");

            var stream = Stream.CreateUserStream();
            stream.TweetCreatedByMe += (sender, args) =>
            {
                Console.WriteLine(args.Tweet);
            };
            stream.StartStreamAsync();
        }
    }
}