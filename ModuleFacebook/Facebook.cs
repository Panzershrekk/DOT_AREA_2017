using System.Collections.Generic;
using JsonApiSerializer;
using Module;
using Newtonsoft.Json;

namespace Module
{
    public class ModuleFacebook : AModule
    {
        public override string GetRequest()
        {
            return JsonConvert.SerializeObject("", new JsonApiSerializerSettings());
        }

        public override string PostRequest()
        {
            return JsonConvert.SerializeObject("", new JsonApiSerializerSettings());
        }
    }
}