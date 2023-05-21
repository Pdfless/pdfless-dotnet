using System.Threading.Tasks;
using Pdfless.Models;

namespace Pdfless
{
    public interface IPdflessClient
    {
        public Task<Response<PdfResponse>> CreateAsync(string templateId, object payload);
    }
}