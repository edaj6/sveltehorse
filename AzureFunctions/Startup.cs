using Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NoSqlDataAccess;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;

[assembly: FunctionsStartup(typeof(AzureFunctions.Startup))]
namespace AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        //in azure function folder: dotnet user-secrets init
        //dotnet user-secrets set "Cosmos:Key" "secret key goes here"
        private const string endpointUrl = "https://jakobcosmos.documents.azure.com:443/";
        private readonly string databaseId = "TestResults";
        private readonly string containerId = "Persons";

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IMyService>((s) =>
            {
                return new MyService();
            });

            var config = new ConfigurationBuilder()                   
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", false)
                   .AddUserSecrets(Assembly.GetExecutingAssembly(), false)
                   .AddEnvironmentVariables()
                   .Build();

            var secretCosmosKey = config["Cosmos:Key"];            
            builder.Services.AddSingleton<Container>(InitializeCosmosClientInstanceAsync(secretCosmosKey).GetAwaiter().GetResult());
            builder.Services.AddSingleton<IQueryHandler<PersonListQuery, Task<Person[]>>, PersonListQueryHandler>();
        }
        
        private async Task<Container> InitializeCosmosClientInstanceAsync(string key)
        {
            try
            {
                CosmosClient client = new CosmosClient(endpointUrl, key);
                var database = await client.CreateDatabaseIfNotExistsAsync(databaseId);
                await database.Database.CreateContainerIfNotExistsAsync(containerId, "/birthday");
                var container = database.Database.GetContainer(containerId);
                return container;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }   
}