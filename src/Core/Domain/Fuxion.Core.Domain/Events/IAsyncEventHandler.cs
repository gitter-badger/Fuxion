using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuxion.Core.Domain.Events
{
    public interface IAsyncEventHandler
    {
        Task HandleAsync(IEvent events);
    }
    public interface IAsyncEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
