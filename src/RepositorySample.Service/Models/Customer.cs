using RepositorySample.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorySample.Service.Models
{
    public class Customer : IAggregateRoot
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

    }
}
