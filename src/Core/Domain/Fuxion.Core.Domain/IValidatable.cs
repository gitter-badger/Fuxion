using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuxion.Core.Domain
{
    public interface IValidatable
    {
        bool IsValid { get; }
    }
}
