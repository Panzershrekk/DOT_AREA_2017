using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using DAO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using JsonApiSerializer;
using Module;
using Newtonsoft.Json;

namespace Module
{
    public class ModuleGmail : AModule
    {
        public GmailService MyService { get; set; }
        string[] Scopes = { GmailService.Scope.GmailReadonly };
        string ApplicationName = "Gmail API .NET Quickstart";

        public ModuleGmail()
        {
            MyService = CreateService();
        }
        
        private GmailService CreateService()
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

        /**
         * Actions
         */
        public string GetLabel()
        {
            var labels = new List<string>();
            var response = MyService.Users.Labels.List("me").Execute();
            if (response != null)
            {
                foreach (var label in response.Labels)
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

        public string GetMessage(string query)
        {
            var result = new List<Message>();
            var request = MyService.Users.Messages.List("me");
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
            var i = 0;
            var msg = new List<string>();
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

        public bool SendMessage(string dest, string subject, string body)
        {
            var fromAddress = new MailAddress("noreply@areadot.net", "From AreaDotNet");
            var toAddress = new MailAddress(dest, "To" + dest);
            const string fromPassword = "azerty--66";

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
            return true;
        }
        
        /**
         * Reactions
         */
        public ReactionResult ReactionSendMessage(User user, string message)
        {
            var result = new ReactionResult();
            if (!SendMessage(user.Email, "[AREA-DOTNET] Notification d'evenement", message))
            {
                result.Message = "Error while sending a message";
                result.Type = ReactionStatus.Error;
            }
            else
                result.Type = ReactionStatus.Ok;
            return result;
        }
    }
}