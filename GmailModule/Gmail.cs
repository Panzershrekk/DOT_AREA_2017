using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading;
using JsonApiSerializer;
using Newtonsoft.Json;
using System.Net;
using System.Reflection.Metadata;

namespace Module
{
    public class GmailModule : AModule
    {
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";
        static public GmailService MyService = CreateService();
        // REQUEST HERE
        public override string GetRequest()
        {
            return JsonConvert.SerializeObject("Hello World!", new JsonApiSerializerSettings());
        }

        public override string PostRequest()
        {
            return JsonConvert.SerializeObject("", new JsonApiSerializerSettings());
        }


        // METHODS HERE
        public string SendMessage(string dest, string sub, string text)
        {
            var fromAddress = new MailAddress("grattepanche.robin@gmail.com", "From Robin");
            var toAddress = new MailAddress(dest, "To" + dest);
            const string fromPassword = "azerty--66";
            string subject = sub;
            string body = text;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return "OK";
        }

        static GmailService CreateService()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }

        // get labels
        public string GetLabel()
        {
            List<string> labels = new List<string>();
            ListLabelsResponse response = MyService.Users.Labels.List("me").Execute();
            if (response != null)
            {
                foreach (Label label in response.Labels)
                {
                    if (label.Id != null)
                       labels.Add(label.Id);
                }
            }
            else
            {
                Console.WriteLine("ERROR: no labels");
                return JsonConvert.SerializeObject("No labels", new JsonApiSerializerSettings());
            }
            return JsonConvert.SerializeObject(labels, new JsonApiSerializerSettings());
        }

        // get messages
        public string GetMessage(string query)
        {
            List<Message> result = new List<Message>();
            UsersResource.MessagesResource.ListRequest request = MyService.Users.Messages.List("me");
            request.Q = query;

            do
            {
                try
                {
                    ListMessagesResponse res = request.Execute();
                    result.AddRange(res.Messages);
                    request.PageToken = res.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
            } while (!String.IsNullOrEmpty(request.PageToken));

            Console.WriteLine("MESSAGES:");
            int i = 0;
            List<string> msg = new List<string>();
            foreach (var r in result)
            {
                if (i <= 5)
                {
                    if (MyService.Users.Messages.Get("me", r.Id).Execute().LabelIds[0] == "UNREAD")
                    {
                        msg.Add("-- Mail --\r\n");
                        msg.Add(MyService.Users.Messages.Get("me", r.Id).Execute().Snippet);
                    }
                    i++;
                }
                else
                    return JsonConvert.SerializeObject(msg, new JsonApiSerializerSettings());

            }
            return JsonConvert.SerializeObject("Empty.", new JsonApiSerializerSettings());
        }

        /*   public static string Base64UrlEncode(string input)
           {
               var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
               return Convert.ToBase64String(inputBytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
           }*/
    }
}
