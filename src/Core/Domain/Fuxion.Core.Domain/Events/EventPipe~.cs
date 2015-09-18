using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fuxion.Core.Domain.Events
{
    //public class EventPipeManager
    //{
    //    private readonly Dictionary<Guid, EventPipe> _dic = new Dictionary<Guid, EventPipe>();
    //    public EventPipe GetPipe(Guid id)
    //    {
    //        return _dic.ContainsKey(id) ? _dic[id] : _dic[id] = new EventPipe();
    //    }
    //}
    //public class EventPipe
    //{
    //    readonly Dictionary<Action<IEvent>, Func<IEvent, bool>> _pipe = new Dictionary<Action<IEvent>, Func<IEvent, bool>>();

    //    public void Process(IEvent @event)
    //    {
    //        foreach (var action in _pipe.Where(action => action.Value(@event)))
    //            action.Key(@event);
    //    }

    //    public void Subscribe<T>(Action<T> action) where T : IEvent
    //    {
    //        Subscribe(_=> action((T)_), evt => evt is T);
    //    }
    //    public void Subscribe(Action<IEvent> action) {
    //        Subscribe(action, _ => true);
    //    }
    //    public void Subscribe(Action<IEvent> action, Func<IEvent,bool> filter)
    //    {
    //        _pipe.Add(action, filter);
    //    }

    //    public void Intercept<T>(Action<T> action) where T : IEvent
    //    {
    //        Intercept(_ => action((T)_), evt => evt is T);
    //    }
    //    public void Intercept(Action<IEvent> action)
    //    {
    //        Intercept(action, _ => true);
    //    }
    //    public void Intercept(Action<IEvent> action, Func<IEvent, bool> filter)
    //    {
    //        _pipe.Add(action, filter);
    //    }
    //}
}
