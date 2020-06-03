using Web.Base.Cqrs.Query;

namespace Web1.Models.Lineas
{
    public class LineaResult : IQueryResult
    {
        public string ItemNumber { get; set; }
        public long BarcodeItem { get; set; }
        public string Descripcion { get; set; }
        public decimal CantidadComprada { get; set; }
        public decimal CantidadPickeada { get; set; }
        public string Estado { get; set; }
        public string TipoInventario { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal PrecioUnitarioProducto { get; set; }
        public string PrecioPagadoPorLinea { get; set; }

    }
}
