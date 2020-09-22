using System.Collections.Generic;

namespace AzureFunctions
{
    public interface IMyService
    {
        List<string> GetPersons();
    }
}