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

        public override string GetRequest()
        {
            dynamic result = "";
            try
            {
                var parameters = new Dictionary<string, object>();
                parameters["fields"] = "id,message";
                result = Fb.Get("/me");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JsonConvert.SerializeObject(result, new JsonApiSerializerSettings());
        }

        public override string PostRequest()
        {
            return JsonConvert.SerializeObject("", new JsonApiSerializerSettings());
        }

        public string PostStatus(string message)
        {
            if (message == "" || message == null)
                return JsonConvert.SerializeObject("need params after post request", new JsonApiSerializerSettings());
            try
            {
                dynamic messagePost = new ExpandoObject();
                messagePost.access_token = Token;
                messagePost.message = message;
                Fb.Post("/me/feed", messagePost);
            }
            catch (Facebook.FacebookOAuthException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch (Facebook.FacebookApiException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return JsonConvert.SerializeObject(message, new JsonApiSerializerSettings());
        }

        public string Webhook(string message)
        {
            return JsonConvert.SerializeObject(message, new JsonApiSerializerSettings());
        }
    }
}