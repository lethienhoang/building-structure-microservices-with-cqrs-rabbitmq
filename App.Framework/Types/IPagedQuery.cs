using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Framework.Types
{
    public interface IPagedQuery : IQuery
    {
        int Page { get; }

        int Results { get; }

        string OrderBy { get; }

        string SortOrder { get; }
    }
}
