using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TiendaIphone.Models;

namespace TiendaIphone.Context
{
    public class TiendaContext : DbContext
    {
        public DbSet<Tienda> Tienda { get; set; }
        public DbSet<Iphone> Iphones { get; set; }
        public DbSet<AccesoriosiPhone> Accesorios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Tienda> tiendaInit = new List<Tienda>();

            //DEFINO LAS TABLAS DE LA TIENDA CON EL MODELBUILDER

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
                iphone.Property(p => p.FechaAltaIphone);
                iphone.Property(p => p.DisponibilidadIphone);
                iphone.Property(p => p.Precio);
                iphone.HasData(iphoneInit);


            });


            List<AccesoriosiPhone> accesoriosInit = new List<AccesoriosiPhone>();

            modelBuilder.Entity<AccesoriosiPhone>(accesorios =>
            {
                accesorios.ToTable("Accesorios");

                accesorios.HasKey(p => p.AccesorioID);
                accesorios.HasOne(p => p.Tienda).WithMany(p => p.ListadoAccesorios).HasForeignKey(p => p.TiendaAccesorioID);
                accesorios.Property(p => p.Modelo).IsRequired().HasMaxLength(100);
                accesorios.Property(p => p.EstadoAccesorio);
                accesorios.Property(p => p.ColorAccesorio);
                accesorios.Property(p => p.Descripcion).HasMaxLength(350);
                accesorios.Property(p => p.Precio);
                accesorios.Property(p => p.FechaAlta);
                accesorios.Property(p => p.DisponibilidadAccesorio);
                accesorios.HasData(accesoriosInit);


            });

            List<Usuario> usuariosInit = new List<Usuario>();

            modelBuilder.Entity<Usuario>(usuarios =>
            {
               
                usuarios.ToTable("Usuario");

                usuarios.Property(p => p.UsuarioNombre);
                usuarios.HasKey(p => p.UsuarioEmail);
                usuarios.Property(p => p.UsuarioContrasenia);
                usuarios.Property(p => p.UsuarioRol);
                usuarios.HasData(usuariosInit);

            });


        }
    }
}
