using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Base.Cqrs.Query;

namespace Web1.Models.Ordenes
{
    public class OrdenesResult : IQueryResult
    {
        public List<OrdenResult> Ordenes { get; }

        public OrdenesResult(IEnumerable<OrdenResult> ordenes)
        {
            Ordenes = ordenes.ToList();
        }
    }
}
