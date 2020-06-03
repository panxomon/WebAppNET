namespace Web.Base.Validador
{
    public class Regla
    {
        public string Propiedad { get; private set; }
        public string Descripcion { get; private set; }

        public Regla(string propiedad, string descripcion)
        {
            Propiedad = propiedad;
            Descripcion = descripcion; 
		}
    }
}
