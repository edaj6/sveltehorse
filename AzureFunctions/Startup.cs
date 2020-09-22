using Domain;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NoSqlDataAccess;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(AzureFunctions.Startup))]
namespace AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IMyService>((s) =>
            {
                return new MyService();
            });

            builder.Services.AddSingleton<IQueryHandler<PersonListQuery, Task<Person[]>>, PersonListQueryHandler>();
        }
    }
}