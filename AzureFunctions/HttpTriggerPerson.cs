using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunctions
{
    public class HttpTriggerPerson
    {
        private readonly IQueryHandler<PersonListQuery, Task<Person[]>> queryHandler;

        public HttpTriggerPerson(IQueryHandler<PersonListQuery, Task<Person[]>> queryHandler)
        {
            this.queryHandler = queryHandler ?? 
                throw new System.ArgumentNullException(nameof(queryHandler));
        }

        [FunctionName("Person")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var people = await queryHandler.Handle(new PersonListQuery());

            var result = (from p in people
                          select new
                          {
                              p.FirstName,
                              p.LastName,
                              p.Birthday,
                              p.Url
                          });

            return new OkObjectResult(result);
        }
    }
}
