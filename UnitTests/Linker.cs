using LinkerModule;
using Module;
using Xunit;

namespace UnitTests
{
    public class LinkerTest
    {
        private Linker linker { get; set; }
        
        public LinkerTest()
        {
            linker = new Linker();
        }
        
        [Fact]
        public void LinkerModuleInitialisation()
        {
            Assert.True(linker.Modules.ContainsKey(typeof(ModuleFacebook)));
            Assert.True(linker.Modules.ContainsKey(typeof(ModuleTwitter)));
            Assert.True(linker.Modules.ContainsKey(typeof(ModuleGmail)));
        }

        [Fact]
        public void LinkerMethodsTranslator()
        {
            Assert.True(linker.ReactionsTranslation.ContainsKey(
                "ReactionSendMessage"));
            Assert.True(linker.ReactionsTranslation.ContainsKey("ReactionTweet"));
        }

        [Fact]
        public void LinkBadTwoActions()
        {
            Assert.Equal(linker.AddLink("Patati", "Patata"), false);
            Assert.Equal(0, linker.Links.Count);
        }

        [Fact]
        public void LinkTwoActions()
        {
            linker.AddLink("FacebookGetRequest", "GmailSendMessage");
        }

        [Fact]
        public void LinkerSimulateActionReactionFacebookGmail()
        {
            
        }
    }
}