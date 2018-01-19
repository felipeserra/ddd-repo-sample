using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositorySample.Framework
{
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        IRepositoryContext Context { get; }

        Task AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveByKeyAsync(Guid key, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateByKeyAsync(Guid key, TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default(CancellationToken));

        Task<TAggregateRoot> FindByKeyAsync(Guid key, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<TAggregateRoot>> FindBySpecificationAsync(Expression<Func<TAggregateRoot, bool>> specification, CancellationToken cancellationToken = default(CancellationToken));
    }
}
