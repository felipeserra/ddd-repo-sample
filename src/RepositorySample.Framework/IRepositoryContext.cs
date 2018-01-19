using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositorySample.Framework
{
    /// <summary>
    /// 表示仓储上下文的概念。
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IRepositoryContext : IDisposable
    {
        /// <summary>
        /// 获取仓储上下文的Id值。
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 获取由当前仓储上下文所使用的基础结构层数据持久化对象。
        /// </summary>
        /// <remarks>
        /// 例如：Entity Framework中的DbContext，或者是MongoDB中的Mongo Client。
        /// </remarks>
        object Session { get; }

        /// <summary>
        /// 获取仓储实例，该仓储的执行上下文将由当前仓储上下文托管。
        /// </summary>
        /// <typeparam name="TAggregateRoot">所需获取的仓储所操作的聚合根的类型。</typeparam>
        /// <returns>作用于指定类型聚合根的仓储实例。</returns>
        IRepository<TAggregateRoot> GetRepository<TAggregateRoot>()
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 异步执行提交操作。
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>实例，用于接收操作取消的信息，以便取消操作。</param>
        /// <returns>执行提交操作的<see cref="Task"/>实例。</returns>
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
