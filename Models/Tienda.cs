namespace TiendaIphone.Models
{
    public class Tienda
    {
       public ICollection<Iphone> ListadoIphone { get; set; }
        
        public ICollection<AccesoriosiPhone> ListadoAccesorios { get; set; }    
        public int ID { get; set; }

        public string NombreTienda { get; set; }    
       
    }
}
