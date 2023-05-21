using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Pdfless.Extensions
{
    public class PdflessHttpMessageHandler : DelegatingHandler
    {
        private readonly ILogger<PdflessHttpMessageHandler> _logger;
        private readonly PdflessOptions _options;
        public PdflessHttpMessageHandler(IOptions<PdflessOptions> options,
            ILogger<PdflessHttpMessageHandler> logger)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add("apikey", _options.ApiKey);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}