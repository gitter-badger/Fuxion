using Fuxion.Core.Domain.Events;
using Fuxion.Core.Domain.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuxion.Core.Domain.Repositories
{
    public abstract class AggregateRepository<T> where T : IAggregate
    {
        public abstract Task<T> FindAsync(Guid id);
        public abstract Task<T> GetAsync(Guid id);
        [Log(typeof(string), typeof(IAggregate), ApplyToStateMachine = true)]
        public async Task SaveAsync(T eventSourced)
        {
            if (!eventSourced.Events.Any()) return;
            // TODO - Oscar - Think in correlationId, how will work and if i need it
            //await OnSaveAsync(eventSourced, correlationId);
            await OnSaveAsync(eventSourced);
            IEvent @event;
            if (eventSourced.Events.Count() > 1) @event = new EventBatch(eventSourced.Events);
            else @event = eventSourced.Events.Single();
            await DomainManager.RaiseAsync(@event);
        }
        protected abstract Task OnSaveAsync(T eventSourced);
    }
}
