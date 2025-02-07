using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace NetCoreAPI_Mongodb.Data
{
    public class MongoDBService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;

        public class MongoDBDatabaseSettings
        {
            public string ConnectionString { get; set; } = null!;

            public string DatabaseName { get; set; } = null!;

            public string EmployeesCollectionName { get; set; } = null!;
        }

        public MongoDBService(IConfiguration configuration, IOptions<MongoDBDatabaseSettings> options)
        {
            _configuration = configuration;

            var mongoClient = new MongoClient(
                    options.Value.ConnectionString);
            //var mongoUrl = MongoUrl.Create(connectionString);
            //var mongoDatabase  = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoDatabase? Database => _database;
    }
}
