using System;

namespace RepositorySample.Framework
{
    /// <summary>
    /// 表示领域驱动设计中实体的概念。
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 获取或设置实体的键值（实体键）。
        /// </summary>
        Guid Id { get; set; }
    }
}
