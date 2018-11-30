using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;

namespace SystemManagementDbService
{
    public interface IDataBaseOperations
    {
        Task<string> Add(Person person);
        Task<Person> GetPerson(string id);
        Task<Person> Add(History history);
        bool Edit(Person person);
        bool Delete(Person person);
        List<Person> GetPersons(string table);
        List<Person> GetUsers(string table);
        List<Person> GetHistory(string table);

    }
}
