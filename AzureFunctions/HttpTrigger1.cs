using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFunctions
{
    public class HttpTrigger1
    {
        private readonly HttpClient _client;
        private readonly IMyService _service;

        public HttpTrigger1(HttpClient httpClient, IMyService service)
        {
            this._client = httpClient;
            this._service = service;
        }

        [FunctionName("Person")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var response = await _client.GetAsync("https://microsoft.com");
            var message = _service.GetPersons();

            var result = new { Message = message, value = "Just added 22-9-20 10:14", Details = "Response from httprequest microsoft.com",  Response = response };

            return new OkObjectResult(result);
        }

        //[FunctionName("Person")]
        //public async Task<IActionResult> RunPerson(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        //    ILogger log)
        //{  

        //    return new OkObjectResult(result);
        //}

    }
}
