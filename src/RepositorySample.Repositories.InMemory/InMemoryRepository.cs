using RepositorySample.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
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
            throw new NotImplementedException();
        }

        public Task<TAggregateRoot> FindByKeyAsync(Guid key, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TAggregateRoot>> FindBySpecificationAsync(Expression<Func<TAggregateRoot, bool>> specification, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task RemoveByKeyAsync(Guid key, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task UpdateByKeyAsync(Guid key, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
