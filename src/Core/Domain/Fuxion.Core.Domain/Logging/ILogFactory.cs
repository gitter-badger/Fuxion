using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuxion.Core.Domain.Logging
{
	public interface ILogFactory
	{
		ILog Create(Type declaringType);
		void Initialize();
	}
}
