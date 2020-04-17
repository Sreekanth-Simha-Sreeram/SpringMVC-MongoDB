using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SpringMvc.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpringMvc.Datalayer
{
  public  class MongoRepository
    {
        //delcaring mongo db
        private readonly IMongoDatabase _database;

        public MongoRepository(IOptions<Settings> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                if (client != null)
                    _database = client.GetDatabase(settings.Value.Database);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to MongoDb server.", ex);
            }

        }

        public IMongoCollection<User> users => _database.GetCollection<User>("Users");

    }
}
