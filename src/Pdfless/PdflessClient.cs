using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pdfless.Commands;
using Pdfless.Models;

namespace Pdfless
{
    public class PdflessClient : IPdflessClient
    {
        private readonly ILogger<PdflessClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly PdflessOptions _options;

        public PdflessClient(ILogger<PdflessClient> logger, HttpClient httpClient, IOptions<PdflessOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options != null ? options.Value : throw new ArgumentNullException(nameof(_options));
        }

        public async Task<Response<PdfResponse>> CreateAsync(string templateId, object payload)
        {
            var url = $"{_options.Host}/{_options.Version}/pdfs";

            var json = new StringContent(
                            JsonSerializer.Serialize(
                                new CreatePDFCommand
                                {
                                    TemplateId = templateId,
                                    Payload = payload
                                }),
                            Encoding.UTF8,
                            "application/json");

            var response = await _httpClient.PostAsync($"{_options.Version}/pdfs", json);
            
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Response<PdfResponse>>(result);
            }

            return null;
        }
    }
}