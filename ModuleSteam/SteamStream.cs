using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
//using Microsoft.XmlDiffPatch;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;
using PortableSteam;
using PortableSteam.Infrastructure;

namespace Module
{
    public class SteamStream
    {
        public SteamStream()
        {
            Task task = new Task(new Action(CompareFriendList));
            task.Start();
        }

        private void CompareFriendList()
        {
            var oldFriendList = SteamWebAPI.General().ISteamUser().GetFriendList(SteamIdentity.FromSteamID(76561198062399869), RelationshipType.All).GetResponseString();
            while (true)
            {
                Thread.Sleep(1000);
                var newFriendList = SteamWebAPI.General().ISteamUser().GetFriendList(SteamIdentity.FromSteamID(76561198062399869), RelationshipType.All).GetResponseString();
                if (!oldFriendList.Equals(newFriendList))
                {
                    checkDifference(newFriendList, oldFriendList);
                    oldFriendList = newFriendList;
                }
            }
        }

        private void checkDifference(string sourceJsonString, string targetJsonString)
        {
            JObject sourceJObject = JsonConvert.DeserializeObject<JObject>(sourceJsonString);
            JObject targetJObject = JsonConvert.DeserializeObject<JObject>(targetJsonString);

            if (!JToken.DeepEquals(sourceJObject, targetJObject))
            {
                foreach (KeyValuePair<string, JToken> sourceProperty in sourceJObject)
                {
                    JProperty targetProp = targetJObject.Property(sourceProperty.Key);

                    if (!JToken.DeepEquals(sourceProperty.Value, targetProp.Value))
                    {
                        var latestFriend = 0;
                        var latestFriendId = "Error";
                        foreach (var test in sourceProperty.Value["friends"])
                        {
                            int current = int.Parse(test["friend_since"].ToString());
                            if (current >= latestFriend)
                            {
                                latestFriend = current;
                                latestFriendId = test["steamid"].ToString();
                            }
                        }
                        Console.WriteLine(latestFriendId);
                    }
                    else
                    {
                        var latestFriend = 0;
                        var latestFriendId = "Error";
                        foreach (var test in sourceProperty.Value["friends"])
                        {
                            int current = int.Parse(test["friend_since"].ToString());
                            if (current >= latestFriend)
                            {
                                latestFriend = current;
                                latestFriendId = test["steamid"].ToString();
                            }
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