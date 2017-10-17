using System;
using MongoDB.Driver;

namespace mongodb_replica_set
{
    public static class CollectionExtensions 
    {
        private readonly static Random random = new Random();

        public static T GetRandom<T>(this IMongoCollection<T> collection)
        {
            return collection.Find(FilterDefinition<T>.Empty)
                .Limit(-1)
                .Skip(random.Next(99))
                .First();
        }
    }
}
