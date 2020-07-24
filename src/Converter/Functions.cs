using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using PuppeteerSharp;

namespace Converter
{
    public static class Functions
    {
        [FunctionName(nameof(Converter))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "pdf")] HttpRequest req)
        {
            var htmlString = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(htmlString))
            {
                return new BadRequestResult();
            }

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new string[] { "--no-sandbox", "--disable-setuid-sandbox" }
            });
            using var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlString);
            var result = await page.PdfDataAsync();
            return new FileContentResult(result, "application/pdf")
            {
                FileDownloadName = "Output.pdf"
            };
        }
    }
}
