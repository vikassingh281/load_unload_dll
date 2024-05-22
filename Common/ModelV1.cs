using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class ModelV1
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
