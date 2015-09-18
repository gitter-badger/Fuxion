using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuxion.Core.Domain
{
    public class FuxionException : Exception
    {
        public FuxionException() : base() { }
        public FuxionException(string message) : base(message) { }
        public FuxionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
