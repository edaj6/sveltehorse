using System.Threading.Tasks;

namespace Domain
{
    public class PersonListQuery : IQuery<Task<Person[]>>
    {
    }
}
