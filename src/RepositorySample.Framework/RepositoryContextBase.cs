using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositorySample.Framework
{
    public abstract class RepositoryContextBase<TSession> : DisposableObject, IRepositoryContext
        where TSession : class
    {
        private readonly TSession session;
        private readonly Guid id = Guid.NewGuid();
        private readonly ConcurrentDictionary<Type, object> cachedRepositories = new ConcurrentDictionary<Type, object>();

        protected RepositoryContextBase(TSession session)
        {
            this.session = session;
        }

        public Guid Id => id;

        public object Session => session;

        public virtual Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public IRepository<TAggregateRoot> GetRepository<TAggregateRoot>()
            where TAggregateRoot : class, IAggregateRoot
            => (IRepository<TAggregateRoot>)cachedRepositories.GetOrAdd(typeof(TAggregateRoot), CreateRepository<TAggregateRoot>());

        protected abstract IRepository<TAggregateRoot> CreateRepository<TAggregateRoot>()
            where TAggregateRoot : class, IAggregateRoot;
    }
}
