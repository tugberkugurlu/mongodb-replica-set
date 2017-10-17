using MongoDB.Bson;

namespace mongodb_replica_set
{
    partial class Program
    {
        public class User 
        {
            public ObjectId Id { get; set; }
            public string Name { get; set; }
        }
    }
}
