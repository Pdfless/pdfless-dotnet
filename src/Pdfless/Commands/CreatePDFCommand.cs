
using System.Text.Json.Serialization;

namespace Pdfless.Commands
{
    public class CreatePDFCommand
    {
        [JsonPropertyName("template_id")]
        public string TemplateId { get; set; }
        
        [JsonPropertyName("payload")]
        public object Payload { get; set; }
    }
}