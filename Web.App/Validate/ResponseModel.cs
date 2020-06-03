using System.Collections.Generic;

namespace Web.App.Validate
{
    public class ResponseModel
    {
        public List<string> Errores { get; set; }
        //public object Resultado { get; set; }

        public ResponseModel(List<string> errores)
        {
            Errores = errores; 
            //Resultado = resultado;
        }
    }
}