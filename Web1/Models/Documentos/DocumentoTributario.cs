using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web1.Models.Documentos
{
    public class DocumentoTributario
    {
        public int Folio { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public bool NotadeCredito { get; set; }
        public long Monto { get; set; }
    }
}
