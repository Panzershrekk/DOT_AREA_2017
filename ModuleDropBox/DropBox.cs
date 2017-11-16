using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using JsonApiSerializer;
using Module;
using Newtonsoft.Json;

namespace Module
{
    public class DropBox : AModule
    {
        private DropboxClient dbx = new DropboxClient("KuxKIuxdEAAAAAAAAAAAMhspYWaGX1xEky3hVlRVkjuxz2PqqQvwPkLDPiCkY2oV");

        public DropBox()
        {
        }

        public override string GetRequest()
        {
            var list = dbx.Files.ListFolderAsync(string.Empty, true);
            return JsonConvert.SerializeObject(list, new JsonApiSerializerSettings());
        }

        public override string PostRequest()
        {
            return JsonConvert.SerializeObject("post dropBox", new JsonApiSerializerSettings());
        }
      
        public void Core()
        {
            while (true)
            {
                var list = dbx.Files.ListFolderAsync(string.Empty, true);

            }
        }

        private string GetSha256Hash(HMACSHA256 sha256Hash, string input)
        {
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            
            var stringBuilder = new StringBuilder();
            
            foreach (byte t in data)
            {
                stringBuilder.Append(t.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public bool VerifySha256Hash(HMACSHA256 sha256Hash, string input, string hash)
        {
            string hashOfInput = GetSha256Hash(sha256Hash, input);

            if (String.Compare(hashOfInput, hash, StringComparison.OrdinalIgnoreCase) == 0)
                return true;

            return false;
        }

        public string GetNameAccount(string acc)
        {
            var ret = dbx.Users.GetAccountAsync(acc);
            return ret.Result.Name.DisplayName;
        }
    }
}
