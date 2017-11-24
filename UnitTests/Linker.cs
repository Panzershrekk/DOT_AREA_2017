using System;
using System.Collections.Generic;
using Api;
using DAO;
using JsonApiSerializer;
using LinkerModule;
using Module;
using Newtonsoft.Json;
using Xunit;

namespace UnitTests
{
    public class LinkerTest
    {
        private Linker Linker { get; set; }
        
        public LinkerTest()
        {
            var modules = new Dictionary<Type, dynamic>
            {
                {typeof(ModuleTwitter), new ModuleTwitter()},
                {typeof(ModuleFacebook), new ModuleFacebook()},
                {typeof(ModuleDropbox), new ModuleDropbox()},
                {typeof(ModuleGmail), new ModuleGmail()},
                {typeof(ModuleSteam), new ModuleSteam()},
            };
            Linker = new Linker(modules);
        }

        [Fact]
        public void SetupLinker()
        {
            Assert.True(true);
        }
        
        [Fact]
        public void LinkerModuleInitialisation()
        {
            Assert.True(Linker.Modules.ContainsKey(typeof(ModuleFacebook)));
            Assert.True(Linker.Modules.ContainsKey(typeof(ModuleTwitter)));
            Assert.True(Linker.Modules.ContainsKey(typeof(ModuleGmail)));
        }

        [Fact]
        public void LinkerMethodsTranslator()
        {
            Assert.True(Linker.ReactionsTranslation.ContainsKey(
                "ReactionSendMessage"));
            Assert.True(Linker.ReactionsTranslation.ContainsKey("ReactionTweet"));
        }

        [Fact]
        public void LinkBadTwoActions()
        {
            Assert.Equal(Linker.AddLink("Patati", "Patata"), false);
            Assert.Equal(Linker.AddLink("Patati", "ReactionSendMessage"), false);
            Assert.Equal(Linker.AddLink("ReactionSendMessage", "Patata"), false);
            Assert.Equal(0, Linker.Links.Count);
        }

        [Fact]
        public void LinkActionReaction()
        {
            Assert.True(Linker.AddLink("TwitterPostRequest", "ReactionSendMessage"));
        }

        [Fact]
        public void LinkerSimulateActionReactionTwitterGmail()
        {
            var user = new User();
            user.Email = "guillobits@gmail.com";
            user.Firstname = "Guillaume";
            user.Lastname = "CAUCHOIS";
            user.Username = "guillobits";
            user.Password = "password";
            // RECEIVE ACTION REQUEST
            const string action = "TwitterPostRequest";
            
            var linkret = Linker.AddLink(action, "ReactionSendMessage");
            
            const string message = "Utest_" + "LinkerSimulateActionReactionFacebookGmail";
            // CALL ALL REACTIONS
            var ret = Linker.ExecuteReactions(action, user, message);
            JsonConvert.SerializeObject(ret, new JsonApiSerializerSettings());
            Assert.True(linkret);
            Assert.True(true);
        }
    }
}