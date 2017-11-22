using System;
using System.Collections.Generic;
using System.Reflection;
using DAO;
using Module;

namespace LinkerModule
{   
    public class Linker
    {
        public Dictionary<Type, dynamic> Modules;
        public Dictionary<string, Delegate> ReactionsTranslation { get; set; }
        public Dictionary<string, string> Links { get; set; }
        public delegate ReactionResult ReactDelegate(User user, string msg);

        /**
         * Initialization
         */
        public Linker(Dictionary<Type, dynamic> modules)
        {
            Modules = modules;
            
            InitializeReactions();
            Links = new Dictionary<string, string>();
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
                Type typeModule = moduleIt.Key;
                var methods = typeModule.GetMethods(BindingFlags.Public | BindingFlags.Instance);
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
            var list = new List<string>();
            foreach (var link in Links)
            {
                if (link.Key == action)
                    list.Add(link.Value);
            }
            return list;
        }

        public List<ReactionResult> ExecuteReactions(string action, User user, string message)
        {
            var reactions = GetReactList(action);
            var results = new List<ReactionResult>();
            
            foreach (var reaction in reactions)
            {
                if (!ReactionsTranslation.ContainsKey(reaction))
                    throw new Exception("Invalid reaction was called");
                var del = ReactionsTranslation[reaction];
                var ret = del.DynamicInvoke(user, message);
                results.Add((ReactionResult) ret);
            }
            return results;
        }
    }
}