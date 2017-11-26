using System.Collections.Generic;
using JsonApiSerializer;
using Module;
using Newtonsoft.Json;
using System.Dynamic;
using System;

namespace Module
{
    public class ModuleFacebook : AModule
    {
        Facebook.FacebookClient Fb;
        string Token = "EAABz4BYjp5YBAKK6NP5gsMZBUYqWKsJI83tduvJdJnZAucZBs3gZBFZBfcvFBb92T3IHPXO8TwLZBFdmTx0iM3lPeEVirrzT3pjjjZAJJRNuLMnlN7WnCGykYorYFAMzDHNSf7cVJJyf9tyLDmEMjqVHBkJWfrgKd7rml3MdSiE9EDdC5JQH8O57E7pgV7Y3p38UqwWoLGTi9R6PkSyZA5ZClRKfchZA5euH7ZAqPqoTsEPQY9YzlwdXvt1";

        public ModuleFacebook()
        {
            Fb = new Facebook.FacebookClient(Token);
            Fb.AppId = "127406281303958";
            Fb.AppSecret = "243701dadc53d3879683e0d8e2bff36c";
        }

        public object FacebookGetMe()
        {
            object result = "";
            try
            {
                result = Fb.Get("/me");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        public bool PostStatus(string message)
        {
            try
            {
                dynamic messagePost = new ExpandoObject();
                messagePost.access_token = Token;
                messagePost.message = message;
                Fb.Post("/me/feed", messagePost);
            }
            catch (Facebook.FacebookOAuthException)
            {
                return false;
            }
            catch (Facebook.FacebookApiException)
            {
                return false;
            }
            return true;
        }

        public string Webhook(string message)
        {
            return JsonConvert.SerializeObject(message, new JsonApiSerializerSettings());
        }
        
        /**
         * Reactions
         */
        public ReactionResult ReactionFacebookPost(DAO.User user, string msg)
        {
            var result = new ReactionResult();
            try
            {
                if (PostStatus(msg))
                    throw new Exception();
                result.Type = ReactionStatus.Ok;
            }
            catch (Exception e)
            {
                result.Type = ReactionStatus.Error;
                result.Message = "Cannot publish to Facebook";
            }
            return result;
        }
    }
}
