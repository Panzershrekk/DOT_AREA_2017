using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using JsonApiSerializer;
using Newtonsoft.Json;

namespace Module
{
    public class ModuleDropbox : AModule
    {
        private readonly DropboxClient dbx = new DropboxClient("KuxKIuxdEAAAAAAAAAAAMhspYWaGX1xEky3hVlRVkjuxz2PqqQvwPkLDPiCkY2oV");

        private static string GetSha256Hash(HMACSHA256 sha256Hash, string input)
        {
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            
            var stringBuilder = new StringBuilder();
            
            foreach (byte t in data)
            {
                stringBuilder.Append(t.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static bool VerifySha256Hash(HMACSHA256 sha256Hash, string input, string hash)
        {
            var hashOfInput = GetSha256Hash(sha256Hash, input);

            return String.Compare(hashOfInput, hash, StringComparison.OrdinalIgnoreCase) == 0;
        }
        
        public Task<ListFolderResult> DropboxGetFolderList()
        {
            return dbx.Files.ListFolderAsync(string.Empty, true);
        }

        public string DropboxGetNameAccount(string acc)
        {
            var ret = dbx.Users.GetAccountAsync(acc);
            return ret.Result.Name.DisplayName;
        }
    }
}
