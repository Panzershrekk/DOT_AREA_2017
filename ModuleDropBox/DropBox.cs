using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using JsonApiSerializer;
using Module;
using Newtonsoft.Json;

namespace Module
{
    public class DropBox : AModule
    {
        DropboxClient dbx = new DropboxClient("KuxKIuxdEAAAAAAAAAAAMhspYWaGX1xEky3hVlRVkjuxz2PqqQvwPkLDPiCkY2oV");

        public DropBox()
        {
        }
        public override string GetRequest()
        {
            var list = dbx.Files.ListFolderAsync(string.Empty);
            return JsonConvert.SerializeObject(list, new JsonApiSerializerSettings());
        }

        public override string PostRequest()
        {
            return "200 OK";
            //  return JsonConvert.SerializeObject("post dropBox", new JsonApiSerializerSettings());
        }

        public string GetFilesFolder(string folder)
        {
            var ret = this.dbx.Files.ListFolderAsync(folder.ToString());
            return JsonConvert.SerializeObject(ret, new JsonApiSerializerSettings());
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
    }
}
