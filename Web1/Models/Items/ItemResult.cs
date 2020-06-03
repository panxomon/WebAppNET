using System;
using Web.Base.Cqrs.Query;

namespace Web1.Models.Items
{
    public class ItemResult : IQueryResult
    {
        //Cantidad de items (productos) hacia comunas por fecha, preguntar si es campaña, el medio por el cual llega (siempre por sg)
        private string Item { get; set; }
        private DateTime FechaCompa { get; set; }
        private string Comuna { get; set; }
        private string Tipo { get; set; }
        private string Medio { get; set; }       
    }
}
