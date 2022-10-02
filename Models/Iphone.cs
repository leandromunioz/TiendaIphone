namespace TiendaIphone.Models
{
    public class Iphone
    {
        public int IphoneID { get; set; }
        public string Modelo { get; set; }

        public int Precio { get; set; } 

        public Estado EstadoIphone { get; set; }

        public Color ColorIphone { get; set; }   

        public string Descripcion { get; set; } 

        public Tienda Tienda { get; set; }

        public int TiendaID { get; set; }

        public DisponibilidadiPhone DisponibilidadIphone { get; set; }

        public DateTime FechaAltaIphone { get; set; }


    }

    public enum Color
    {
        Blanco,
        Negro,
        Rojo,
        Dorado,
        Plata
    }

    public enum Estado
    {
        Usado,
        Reacondicionado,
        Sellado
    }

    public enum DisponibilidadiPhone
    {
        Disponible,
        Agotado
    }

}
