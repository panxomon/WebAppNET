using System;
using System.Collections.Generic;
using Web.Base.Cqrs.Query;
using Web1.Models.Comunes;
using Web1.Models.Lineas;
using Web1.Models.ShippingGroups;

namespace Web1.Models.Ordenes
{
    public interface IOrdenResult : IQueryResult
    {
        CanalDeVenta CanalDeVenta { get; set; }
        DateTime FechaCompra { get; set; }
        IList<LineaResult> Lineas { get; set; }
        string NombreCliente { get; set; }
        string Numero { get; set; }
        string RutCliente { get; set; }
        ShippingGroupResult ShippingGroup { get; set; }
    }
}