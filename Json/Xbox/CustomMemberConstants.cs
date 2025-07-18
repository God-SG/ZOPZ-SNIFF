using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZOPZ_SNIFF.Json.Xbox
{
    public class CustomMemberConstants
    {
        [JsonPropertyName("clientCapability")]
        public int ClientCapability { get; set; }
    }
}
