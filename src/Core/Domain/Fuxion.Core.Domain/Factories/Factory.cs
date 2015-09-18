using Fuxion.Core.Domain.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuxion.Core.Domain.Factories
{
    public static class Factory
    {
        static Factory()
        {
            ReturnDefaultValueIfCanNotBeCreated = true;
            _pipe = new IFactory[] {}.ToImmutableList();
        }
        private static ImmutableList<IFactory> _pipe;
        public static bool ReturnDefaultValueIfCanNotBeCreated { get; set; }
        public static void AddToPipe(IFactory factory)
        {
            _pipe = _pipe.Add(factory);
        }
        public static void InsertToPipe(int index, IFactory factory)
        {
            _pipe = _pipe.Insert(index, factory);
        }
        public static T Create<T>() { return (T)Create(typeof(T)); }
        public static object Create(Type type)
        {
            foreach (var fac in _pipe)
            {
                try
                {
                    return fac.Create(type);
                }
                catch { }
            }
            try {
                return Activator.CreateInstance(type);
            }catch(Exception ex)
            {
                Debug.WriteLine("");
                throw;
            }
        }
        public static IEnumerable<T> GetAllInstances<T>()
        {
            return _pipe.SelectMany(fac => fac.GetAllInstances(typeof(T)).Cast<T>());
        }
		public static IEnumerable<object> GetAllInstances(Type type)
		{
			return _pipe.SelectMany(fac => fac.GetAllInstances(type));
		}
	}
}
