using MongoDB.Driver;
using RepositorySample.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RepositorySample.Repositories.MongoDB
{
    public sealed class MongoRepositoryContext : RepositoryContextBase<IMongoClient>
    {
        private readonly MongoRepositorySettings settings;

        public MongoRepositoryContext(MongoRepositorySettings settings)
            : base(new MongoClient(settings.ClientSettings))
        {
            this.settings = settings;
        }

        internal MongoRepositorySettings Settings => settings;

        protected override IRepository<TAggregateRoot> CreateRepository<TAggregateRoot>()
            => new MongoRepository<TAggregateRoot>(this);
    }
}
