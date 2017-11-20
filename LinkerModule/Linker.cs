using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DAO;
using JsonApiSerializer;
using Module;
using Newtonsoft.Json;

namespace LinkerModule
{   
    public class Linker
    {
        public Dictionary<Type, dynamic> Modules { get; set; }
        public Dictionary<string, Delegate> ReactionsTranslation { get; set; }
        public Dictionary<string, Type> TypeTranslDictionary { get; set; }
        public Dictionary<string, string> Links { get; set; }
        public delegate ReactionResult ReactDelegate(User user, string msg);

        /**
         * Initialization
         */
        public Linker()
        {
            InitializeModule();    
            InitializeReactions();
            Links = new Dictionary<string, string>();
        }
        
        private void InitializeModule()
        {
            Modules = new Dictionary<Type, dynamic>
            {
                {typeof(ModuleFacebook), new ModuleFacebook()},
                {typeof(ModuleTwitter), new ModuleTwitter()},
                {typeof(ModuleGmail), new ModuleGmail()}
            };
        }
        
        private void InitializeReactions()
        {
            ReactionsTranslation = new Dictionary<string, Delegate>();
            foreach (var module in Modules)
            {
                var t = module.Key;
                var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (var method in methods)
                {   
                    if (!method.Name.StartsWith("Reaction")) continue;
                    var deleg = (ReactDelegate) method.CreateDelegate(typeof(ReactDelegate), module.Value);
                    if (deleg == null) continue;
                    var reactDelegate = (ReactDelegate) deleg;
                    ReactionsTranslation.Add(method.Name, reactDelegate);
                }
            }
        }
        
        /**
         * Action / Reaction handling
         */
        public bool AddLink(string action, string reaction)
        {
            bool actionFound = false, reactionFound = false;
            
            foreach (var moduleIt in Modules)
            {
                var module = moduleIt.Value;
                var methods = typeof(AModule).GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (var method in methods)
                {
                    actionFound = actionFound || method.Name.Equals(action);
                    reactionFound = reactionFound || method.Name.Equals(reaction);
                }
            }
            if (!actionFound || !reactionFound)
                return false;
            Links.Add(action, reaction);
            return true;
        }

        private IEnumerable<string> GetReactList(string action)
        {
            return (from pair in Links where pair.Key == action select pair.Value).ToList();
        }

        public string ExecuteReactionsAndGetJsonResult(string action, string message)
        {
            var reactions = GetReactList(action);
            var results = new List<ReactionResult>();
            
            foreach (var reaction in reactions)
            {
                ReactionsTranslation[action].DynamicInvoke(new User(), "message");
                //TODO : Gestion des retour de réactions
            }
            return JsonConvert.SerializeObject(reactions, new JsonApiSerializerSettings());;
        }
    }
}