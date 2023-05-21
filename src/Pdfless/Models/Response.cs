using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pdfless.Models
{
    public class Response<T> 
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}