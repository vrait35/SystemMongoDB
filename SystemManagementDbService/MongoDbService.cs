using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SystemManagementDbService
{
    public class MongoDbService : IDataBaseOperations
    {
        private string connectionString;
        private MongoClient client;
        private IMongoDatabase database;
        public MongoDbService(string db,string connectionString1)
        {
            connectionString = connectionString1;
            client= new MongoClient(connectionString);
            database= client.GetDatabase(db);
        }
   
        public async Task<string> Add(Person person)
        {                                        
                var collection = database.GetCollection<BsonDocument>("people");
                BsonDocument person1 = new BsonDocument
            {
                 {"FirstName", person.FirstName},
                 {"LastName", person.LastName},
                 {"MiddleName",person.MiddleName},
                 {"PhoneNumber",person.PhoneNumber},
                 {"Group",person.Group},
            };
                await collection.InsertOneAsync(person1);
            
            var filter = new BsonDocument();
            var people = await collection.Find(filter).ToListAsync();          
            foreach (var doc in people)
            {               
                if (doc["FirstName"].AsString == person.FirstName && doc["LastName"].AsString == person.LastName
                    && doc["Group"].AsString==person.Group)
                {
                    return (doc["_id"].ToString());
                }              
            }
            return null;
        }

        public async Task<Person> Add(History history)
        {
            var collection = database.GetCollection<BsonDocument>("history");
            BsonDocument person1 = new BsonDocument
            {
                 {"IdPerson", history.Id},
                 {"DateTime", history.DateTime.ToString()}                
            };
            await collection.InsertOneAsync(person1);
            Person person = GetPerson(history.Id).GetAwaiter().GetResult();
            if (person.MiddleName.Length > 0)
            {
                return person;
            }
            return null;
        }
        public async Task<Person> GetPerson(string id)
        {
            var collection = database.GetCollection<BsonDocument>("people");
            var filter = new BsonDocument();

            var people = await collection.Find(filter).ToListAsync();
            Person person = new Person();
            int count = 0;
            foreach (var doc in people)
            {               
                if (doc["_id"].ToString() == id )
                {
                    person.FirstName = doc["FirstName"].AsString;
                    person.LastName = doc["LastName"].AsString;
                    person.MiddleName = doc["MiddleName"].AsString;
                    person.PhoneNumber = doc["PhoneNumber"].AsString;
                    person.Group = doc["Group"].AsString;
                    person.Id = id;
                    count++;
                }               
            }
            if (count > 0)
            {
                return person;
            }
            return null;
        }
        public bool Delete(Person person)
        {
            return false;
        }

        public bool Edit(Person person)
        {
            return false;
        }

        public List<Person> GetHistory(string table)
        {
            return null;
        }

        public List<Person> GetPersons(string table)
        {
            return null;
        }

        public List<Person> GetUsers(string table)
        {
            return null;
        }
    }
}
