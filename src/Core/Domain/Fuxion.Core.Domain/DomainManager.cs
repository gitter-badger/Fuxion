using Fuxion.Core.Domain.Commands;
using Fuxion.Core.Domain.Events;
using Fuxion.Core.Domain.Factories;
using Fuxion.Core.Domain.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
namespace Fuxion.Core.Domain
{
    public static class DomainManager
	{
        #region Events
        [Log(typeof(IEvent), ApplyToStateMachine = true)]
        internal static async Task RaiseAsync(IEvent @event)
        {
            var asyncEventHandlerType = typeof(IAsyncEventHandler<>).MakeGenericType(@event.GetType());
            foreach (var evt in Factory.GetAllInstances(typeof(IAsyncEventHandler<>).MakeGenericType(@event.GetType())))
                await (Task)asyncEventHandlerType.GetRuntimeMethod("HandleAsync", new[] { @event.GetType() }).Invoke(evt, new object[] { @event });

            foreach (var evt in Factory.GetAllInstances<IAsyncEventHandler>())
                await evt.HandleAsync(@event);
        }
        #endregion
        #region Commands
        [Log(typeof(ICommand), ApplyToStateMachine = true)]
        public static async Task DoAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var async = Factory.GetAllInstances<IAsyncCommandHandler<TCommand>>();
            if (async.Count() != 1) throw new NotImplementedException($"{async.Count()} handlers found for command {typeof(TCommand).Name}, no zero and no many supported, only can be one.");
            await async.Single().HandleAsync(command);
        }
        #endregion
    }
    
}
