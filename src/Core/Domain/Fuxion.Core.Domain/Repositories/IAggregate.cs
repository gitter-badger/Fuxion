using Fuxion.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuxion.Core.Domain.Repositories
{
    public interface IAggregate
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IEvent> Events { get; }
    }
}
