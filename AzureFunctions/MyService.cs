using System.Collections.Generic;

namespace AzureFunctions
{
    public class MyService : IMyService
    {
        public List<string> GetPersons()
        {
            return new List<string> { "Paul", "Bob", "Siri", "Ny 2109 16.16", "Great names from My DI Service :-)"};
        }
    }
}
