using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Accenture_V1.Models
{
    public class RetornoCotacao
    {
            public string name { get; set; }
            [System.Text.Json.Serialization.JsonPropertyName("base")]
            public string _base { get; set; }
            public string quote { get; set; }
            public float price { get; set; }
            public float price_usd { get; set; }
            public float volume { get; set; }
            public float volume_usd { get; set; }
            public int time { get; set; }

    }
    
}

