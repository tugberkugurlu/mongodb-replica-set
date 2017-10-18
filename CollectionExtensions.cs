using System;
using MongoDB.Driver;
using Polly;

namespace mongodb_replica_set
{
    public static class CollectionExtensions 
    {
        private readonly static Random random = new Random();

        public static T GetRandom<T>(this IMongoCollection<T> collection) 
        {
            var retryPolicy = Policy
                .Handle<MongoCommandException>()
                .Or<MongoConnectionException>()
                .WaitAndRetry(2, retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) 
                );

            return retryPolicy.Execute(() => GetRandomImpl(collection));
        }

        private static T GetRandomImpl<T>(this IMongoCollection<T> collection)

        {
            return collection.Find(FilterDefinition<T>.Empty)
                .Limit(-1)
                .Skip(random.Next(99))
                .First();
        }
    }
}
