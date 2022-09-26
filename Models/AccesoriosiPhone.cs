namespace TiendaIphone.Models
{
    public class AccesoriosiPhone
    {
        public int AccesorioID { get; set; }
        public string Modelo { get; set; }

        public int Precio { get; set; }

        public Estado EstadoAccesorio { get; set; }

        public ColorAccesorio ColorAccesorio { get; set; }

        public string Descripcion { get; set; }

        public Tienda Tienda { get; set; }

        public int TiendaAccesorioID { get; set; }

    }

    public enum ColorAccesorio
    {
        Blanco,
        Negro,
       
    }

    public enum EstadoAccesorio
    {
        Usado,
        Sellado
    }
}

