using System;
using System.Collections.Generic;
using System.Text;
using Module;
using Xunit;

namespace UnitTests
{
    public class Steam
    {
        [Fact]
        public void TwitterGet()
        {
            var module = new ModuleSteam();

            var response = module.SteamGetFriendList();
            Assert.NotEqual(response, "Error");
        }
    }
}
