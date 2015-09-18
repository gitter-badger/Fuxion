using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fuxion.Core.Domain;
using Fuxion.Core.Domain.Events;
using Fuxion.Core.Domain.Factories;
using Fuxion.Core.Domain.Repositories;

namespace Fuxion.Core.Domain.Repositories
{
    public class MemoryAggregateRepository<T> : AggregateRepository<T> where T : class, IAggregate
    {
        public MemoryAggregateRepository()
        {
            _entityFactory = (id, events) => (T)Activator.CreateInstance(typeof(T), id, events);
        }
        readonly List<IEvent> _list = new List<IEvent>();
        private readonly Func<Guid, IEnumerable<IEvent>, T> _entityFactory;
        public override Task<T> FindAsync(Guid id)
        {
            var res = _list.Where(evt => evt.SourceId == id);
            var rr =  _entityFactory.Invoke(id, res);
            return Task.FromResult(rr);
        }
        public override async Task<T> GetAsync(Guid id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
                throw new AggregateNotFoundException(id, typeof(T).Name);
            return entity;
        }
        protected override Task OnSaveAsync(T eventSourced)
        {
            Debug.WriteLine($"MemoryEventSourcedRepository<{typeof(T).Name}>.Save(T eventSourced) - Have {eventSourced.Events.Count()} events to save");
            _list.AddRange(eventSourced.Events);
            return Task.FromResult(0);
        }
    }
}
