using RepositorySample.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RepositorySample.Repositories.InMemory
{
    internal sealed class InMemoryRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        internal InMemoryRepository(IRepositoryContext repositoryContext)
        {
            this.Context = repositoryContext;
        }

        public IRepositoryContext Context { get; }

        public Task AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (InMemoryRepositoryContext.storage.TryGetValue(typeof(TAggregateRoot), out var dictionary))
            {
                if (!dictionary.ContainsKey(aggregateRoot.Id))
                {
                    dictionary.Add(aggregateRoot.Id, aggregateRoot);
                }
            }
            else
            {
                InMemoryRepositoryContext.storage.TryAdd(typeof(TAggregateRoot), new Dictionary<Guid, object> { { aggregateRoot.Id, aggregateRoot } });
            }

            return Task.CompletedTask;
        }

        public Task<TAggregateRoot> FindByKeyAsync(Guid key, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (InMemoryRepositoryContext.storage.TryGetValue(typeof(TAggregateRoot), out var dictionary))
            {
                if (dictionary.ContainsKey(key))
                {
                    return Task.FromResult(dictionary[key] as TAggregateRoot);
                }

                return Task.FromResult(default(TAggregateRoot));
            }

            return Task.FromResult(default(TAggregateRoot));
        }

        public Task<IEnumerable<TAggregateRoot>> FindBySpecificationAsync(Expression<Func<TAggregateRoot, bool>> specification, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (InMemoryRepositoryContext.storage.TryGetValue(typeof(TAggregateRoot), out var dictionary))
            {
                return Task.FromResult(dictionary.Values.Select(p=> p as TAggregateRoot).Where(specification.Compile()));
            }

            return Task.FromResult(default(IEnumerable<TAggregateRoot>));
        }

        public Task RemoveByKeyAsync(Guid key, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (InMemoryRepositoryContext.storage.TryGetValue(typeof(TAggregateRoot), out var dictionary))
            {
                if (dictionary.ContainsKey(key))
                {
                    dictionary.Remove(key);
                }
            }

            return Task.CompletedTask;
        }

        public Task UpdateByKeyAsync(Guid key, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (InMemoryRepositoryContext.storage.TryGetValue(typeof(TAggregateRoot), out var dictionary))
            {
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key] = aggregateRoot;
                }
            }

            return Task.CompletedTask;
        }
    }
}
