using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositorySample.Repositories.MongoDB
{
    public sealed class MongoRepositorySettings
    {
        public const int DefaultPortNumber = 27017;

        public MongoRepositorySettings(string host, string database)
            : this(host, DefaultPortNumber, database)
        { }

        public MongoRepositorySettings(string host, int port, string database)
        {
            this.ClientSettings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host, port)
            };

            this.Database = database;
        }

        public MongoClientSettings ClientSettings { get; }

        public string Database { get; }
    }
}
