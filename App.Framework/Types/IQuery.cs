using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Framework.Types
{
    public interface IQuery
    {
    }

    public interface IQuery<T> : IQuery
    {
    }
}
