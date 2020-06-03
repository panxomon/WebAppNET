using System;
using System.Collections.Generic;
using Web.Base.Cqrs.Query;
using Web1.Models.Comunes;
using Web1.Models.Lineas;
using Web1.Models.ShippingGroups;

namespace Web1.Models.Ordenes
{
    public class OrdenResult : IQueryResult, IOrdenResult 
    {
        public string Numero { get; set; }
        public DateTime FechaCompra { get; set; }
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public CanalDeVenta CanalDeVenta { get; set; }
        public ShippingGroupResult ShippingGroup { get; set; }
        public IList<LineaResult> Lineas { get; set; }
    }
}
