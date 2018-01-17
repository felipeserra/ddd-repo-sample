using MongoDB.Driver;
using RepositorySample.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositorySample.Repositories.MongoDB
{
    internal sealed class MongoRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        private readonly IMongoCollection<TAggregateRoot> collection;

        internal MongoRepository(IRepositoryContext repositoryContext)
        {
            this.Context = repositoryContext;

            var mongoRepositoryContext = (MongoRepositoryContext)repositoryContext;
            var mongoClient = (IMongoClient)mongoRepositoryContext.Session;

            var database = mongoClient.GetDatabase(mongoRepositoryContext.Settings.Database);
            var collectionName = typeof(TAggregateRoot).Name;
            this.collection = database.GetCollection<TAggregateRoot>(collectionName);
        }

        public IRepositoryContext Context { get; }

        public async Task AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default(CancellationToken))
        {
            var options = new InsertOneOptions { BypassDocumentValidation = true };
            await this.collection.InsertOneAsync(aggregateRoot, options, cancellationToken);
        }

        public async Task<TAggregateRoot> FindByKeyAsync(Guid key, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await this.collection.FindAsync(x => x.Id.Equals(key), cancellationToken: cancellationToken)).FirstOrDefault();
        }

        public async Task<IEnumerable<TAggregateRoot>> FindBySpecificationAsync(Expression<Func<TAggregateRoot, bool>> specification, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (await this.collection.FindAsync(specification, cancellationToken: cancellationToken)).ToListAsync();
        }

        public async Task RemoveByKeyAsync(Guid key, CancellationToken cancellationToken = default(CancellationToken))
        {
            var filterDefinition = Builders<TAggregateRoot>.Filter.Eq(x => x.Id, key);
            await this.collection.DeleteOneAsync(filterDefinition, cancellationToken);
        }

        public async Task UpdateByKeyAsync(Guid key, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default(CancellationToken))
        {
            var filterDefinition = Builders<TAggregateRoot>.Filter.Eq(x => x.Id, key);
            await this.collection.ReplaceOneAsync(filterDefinition, aggregateRoot, cancellationToken: cancellationToken);
        }
    }
}
