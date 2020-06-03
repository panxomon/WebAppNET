using System;

namespace Web1.Models.ShippingGroups
{
    public interface IShippingGroupsPorFecha
    {
        DateTime FechaFin { get; set; }
        DateTime FechaInicio { get; set; }
    }
}