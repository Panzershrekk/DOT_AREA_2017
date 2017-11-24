using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortableSteam;

namespace Api
{
    public class SteamStream
    {
        public SteamStream()
        {
            SteamWebAPI.SetGlobalKey("535AB6031548709DFEEB34CABB80601E");
            var task = new Task(CompareFriendList);
            task.Start();
        }

        private static void CompareFriendList()
        {
            var oldFriendList = SteamWebAPI.General().
                ISteamUser().
                GetFriendList(SteamIdentity.FromSteamID(76561198062399869), RelationshipType.All).
                GetResponseString();
            while (true)
            {
                Thread.Sleep(1000);
                var newFriendList = SteamWebAPI.General()
                    .ISteamUser()
                    .GetFriendList(SteamIdentity.FromSteamID(76561198062399869), RelationshipType.All)
                    .GetResponseString();
                if (oldFriendList.Equals(newFriendList)) continue;
                CheckDifference(newFriendList, oldFriendList);
                oldFriendList = newFriendList;
            }
        }

        private static void CheckDifference(string sourceJsonString, string targetJsonString)
        {
            var sourceJObject = JsonConvert.DeserializeObject<JObject>(sourceJsonString);
            var targetJObject = JsonConvert.DeserializeObject<JObject>(targetJsonString);

            if (!JToken.DeepEquals(sourceJObject, targetJObject))
            {
                foreach (var sourceProperty in sourceJObject)
                {
                    var targetProp = targetJObject.Property(sourceProperty.Key);

                    if (!JToken.DeepEquals(sourceProperty.Value, targetProp.Value))
                    {
                        var latestFriend = 0;
                        var latestFriendId = "Error";
                        foreach (var test in sourceProperty.Value["friends"])
                        {
                            var current = int.Parse(test["friend_since"].ToString());
                            if (current < latestFriend) continue;
                            latestFriend = current;
                            latestFriendId = test["steamid"].ToString();
                        }
                        Console.WriteLine(latestFriendId);
                    }
                    else
                    {
                        var latestFriend = 0;
                        var latestFriendId = "Error";
                        foreach (var test in sourceProperty.Value["friends"])
                        {
                            var current = int.Parse(test["friend_since"].ToString());
                            if (current < latestFriend) continue;
                            latestFriend = current;
                            latestFriendId = test["steamid"].ToString();
                        }
                        Console.WriteLine(latestFriendId);
                    }
                }
            }
            else
            {
                Console.WriteLine("Objects are same");
            }
        }
    }
}