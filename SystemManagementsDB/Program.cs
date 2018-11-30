using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemManagementDbService;

namespace prog
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString= "mongodb://localhost";
            const string db = "dbshag";
            Person person = new Person() {
                FirstName = "andrei",
                LastName = "andreevich",
                MiddleName = "sashov",
                PhoneNumber = "34534",
                Group = "sep172"
            };
            MongoDbService mongoDbService = new MongoDbService(db,connectionString);
            string str=mongoDbService.Add(person).GetAwaiter().GetResult(); 
            Console.WriteLine(str);
            History history = new History()
            {
                Id = "5c00c90020a1493064f348ed",
                DateTime = DateTime.Now
            };
           person= mongoDbService.Add(history).GetAwaiter().GetResult();
            if (person != null)
            {
                Console.WriteLine(person.MiddleName);
            }
            else Console.WriteLine("netu takogo chela");
        }
    }
}
