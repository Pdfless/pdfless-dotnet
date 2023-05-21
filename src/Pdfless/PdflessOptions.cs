namespace Pdfless
{
    public class PdflessOptions
    {
        public PdflessOptions()
        {
            Host = "https://api.pdfless.com";
        }

        public const string PdflessSettingsKey = "Pdfless";
        public string Host { get; set; }
        public string ApiKey { get; set; }
        public string Version { get; set; } = "v1";
    }
}