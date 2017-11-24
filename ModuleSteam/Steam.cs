using PortableSteam;

namespace Module
{
    public class ModuleSteam : AModule
    {
        public void SteamHandleNewFriend()
        {
        }
        
        public string SteamGetFriendList()
        {
            SteamWebAPI.SetGlobalKey("535AB6031548709DFEEB34CABB80601E");
            var response = SteamWebAPI.General().ISteamUser().GetFriendList(SteamIdentity.FromSteamID(76561198062399869), RelationshipType.All).GetResponseString();
            return (response);
        }
    }
}