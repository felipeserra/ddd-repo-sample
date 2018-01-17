using System;
using System.Collections.Generic;
using System.Text;

namespace RepositorySample.Framework
{
    /// <summary>
    /// 表示领域驱动设计中“聚合根”的概念。
    /// </summary>
    /// <seealso cref="RepositorySample.Framework.IEntity" />
    public interface IAggregateRoot : IEntity
    {
    }
}
