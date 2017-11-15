using Tweetinvi;
using System;

namespace Module
{
    public class TwitterStreaming
    {

        public TwitterStreaming()
        {
            var stream = Stream.CreateUserStream();
            stream.TweetCreatedByMe += (sender, args) =>
            {
                Console.WriteLine(args.Tweet);
            };
            stream.StartStreamAsync();
        }
    }
}