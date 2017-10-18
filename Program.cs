using System;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;

namespace mongodb_replica_set
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var settings = new MongoClientSettings
            {
                Servers = new[]
               {
                    new MongoServerAddress("mongo-node1", 27017),
                    new MongoServerAddress("mongo-node2", 27017),
                    new MongoServerAddress("mongo-node3", 27017)
                },
                ConnectionMode = ConnectionMode.ReplicaSet,
                ReplicaSetName = "rs0"
            };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("mydatabase");
            var collection = database.GetCollection<User>("users");

            System.Console.WriteLine("Cluster Id: {0}", client.Cluster.ClusterId);
            client.Cluster.DescriptionChanged += (object sender, ClusterDescriptionChangedEventArgs foo) => 
            {
                System.Console.WriteLine("New Cluster Id: {0}", foo.NewClusterDescription.ClusterId);
            };

            for (int i = 0; i < 100; i++)
            {
                var user = new User { Id = ObjectId.GenerateNewId(), Name = new Bogus.Faker().Name.FullName() };
                collection.InsertOne(user);
            }

            while (true)
            {
                var randomUser = collection.GetRandom();
                Console.WriteLine(randomUser.Name);

                Thread.Sleep(500);
            }
        }
    }
}
