using System;
using System.Text.Json.Serialization;

namespace Pdfless.Models
{
    public class PdfResponse
    {
        [JsonPropertyName("reference_id")]
        public string ReferenceId { get; set; }
        [JsonPropertyName("template_id")]
        public Guid TemplateId { get; set; }
        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; }
        [JsonPropertyName("expires")]
        public DateTimeOffset Expires { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}