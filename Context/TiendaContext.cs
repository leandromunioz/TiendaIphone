using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TiendaIphone.Models;

namespace TiendaIphone.Context
{
    public class TiendaContext : DbContext
    {
        public DbSet<Tienda> Tienda { get; set; }
        public DbSet<Iphone> Iphones { get; set; }

        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Tienda> tiendaInit = new List<Tienda>();


            modelBuilder.Entity<Tienda>(tienda =>
            {
                tienda.ToTable("Tienda");

                tienda.HasKey(p => p.ID);

                tienda.Property(p => p.NombreTienda);

                tienda.HasData(tiendaInit);

            });

            List<Iphone> iphoneInit = new List<Iphone>();

            modelBuilder.Entity<Iphone>(iphone =>
            {
                iphone.ToTable("Iphones");

                iphone.HasKey(p => p.IphoneID);
                iphone.HasOne(p => p.Tienda).WithMany(p => p.ListadoIphone).HasForeignKey(p => p.TiendaID);

                iphone.Property(p => p.Modelo).IsRequired().HasMaxLength(100);
                iphone.Property(p => p.EstadoIphone);
                iphone.Property(p => p.ColorIphone);
                iphone.Property(p => p.Descripcion).HasMaxLength(350);
                iphone.Property(p => p.Precio);
                iphone.HasData(iphoneInit);


            });
        }
    }
}
