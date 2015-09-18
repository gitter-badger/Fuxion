using System;
using System.Collections;
using System.Collections.Generic;

namespace Fuxion.Core.Domain.Factories
{
    public interface IFactory
    {
        object Create(Type type);
		IEnumerable<object> GetAllInstances(Type type);
	}
}