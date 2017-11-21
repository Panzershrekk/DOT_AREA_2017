using System.IO;
using System.Linq;
using Dapper;
using Module;
using Xunit;

namespace UnitTests
{
    public class BattleTest
    {
        [Fact]
        public void BattleGet()
        {
            var module = new Module.ModuleBattlenet();

            var response = module.GetRequest();
            Assert.NotEqual(response, "Error");
        }
    }
}