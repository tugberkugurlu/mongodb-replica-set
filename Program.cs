using System;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mongodb_replica_set
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient();
            var database = client.GetDatabase("mydatabase");
            var collection = database.GetCollection<User>("users");

            for (int i = 0; i < 100; i++)
            {
                var user = new User { Id = ObjectId.GenerateNewId(), Name = new Bogus.Faker().Name.FullName() };
                collection.InsertOne(user);
            }

            while(true) 
            {
                var randomUser = collection.GetRandom();
                Console.WriteLine(randomUser.Name);

                Thread.Sleep(500);
            }
        }
    }
}
