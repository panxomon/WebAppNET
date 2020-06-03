using System;
using Web.Base.Cqrs.Query;
using Web1.Models.Documentos;

namespace Web1.Models.ShippingGroups
{
    public class ShippingGroupResult : IQueryResult
    {
        public int SgId { get; set; }
        public string ShippingGroup { get; set; }
        public string FechaPicking { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public string Estado { get; set; }
        public bool EsPadre { get; set; }
        public decimal PrecioDespacho { get; set; }
        public decimal PrecioTotal { get; set; }
        public string RegionDespacho { get; set; }
        public string ComunaDespacho { get; set; }
        public string DireccionDespacho { get; set; }
        public DocumentoTributario DocumentoTributario { get; set; }

    }
}
