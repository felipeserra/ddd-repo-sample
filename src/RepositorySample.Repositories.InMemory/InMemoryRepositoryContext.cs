using RepositorySample.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RepositorySample.Repositories.InMemory
{
    public sealed class InMemoryRepositoryContext : RepositoryContextBase<ConcurrentDictionary<Type, Dictionary<Guid, object>>>
    {
        internal static readonly ConcurrentDictionary<Type, Dictionary<Guid, object>> storage = new ConcurrentDictionary<Type, Dictionary<Guid, object>>();

        public InMemoryRepositoryContext()
            : base(storage)
        {

        }

        protected override IRepository<TAggregateRoot> CreateRepository<TAggregateRoot>()
        {
            return new InMemoryRepository<TAggregateRoot>(this);
        }

    }
}
