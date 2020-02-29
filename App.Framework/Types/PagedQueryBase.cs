using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Framework.Types
{
    public class PagedQueryBase : IPagedQuery
    {
        public int Page { get; set; }

        public int Results { get; set; }

        public string OrderBy { get; set; }

        public string SortOrder { get; set; }
    }
}
