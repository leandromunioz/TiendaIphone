using Microsoft.EntityFrameworkCore;
using TiendaIphone.Models;
namespace TiendaIphone.Logica
{
    public class LO_Usuario
    {
        //public List<Usuario> ListaUsuarios()
        //{
        //    return new List<Usuario>()
        //    {
        //        new Usuario(){ UsuarioNombre = "Hernando",UsuarioEmail="hernan2@yahoo.com", UsuarioContrasenia="Contrasenia",UsuarioRol= "SuperAdmin" },
        //        new Usuario(){ UsuarioNombre = "Fernando",UsuarioEmail="fernandomirando@yahoo.com", UsuarioContrasenia="Contrasenia",UsuarioRol= "SuperAdmin" },
        //        new Usuario(){ UsuarioNombre = "Quique",UsuarioEmail="quique@yahoo.com", UsuarioContrasenia="Contrasenia",UsuarioRol= "SuperAdmin"},
        //        new Usuario(){ UsuarioNombre = "Jorgeardo",UsuarioEmail="jorgeelcapo@yahoo.com", UsuarioContrasenia="Contrasenia",UsuarioRol= "SuperAdmin" },


        //        };
        //}

        public static Usuario ValidarUsuario(string correo, string contrasenia, string usuario ,DbSet<Usuario> ListaUsuarios)
       {
            //Comparacion con data almacenada y devolucion del primer match o null

            return ListaUsuarios.Where(item => (item.UsuarioEmail == correo || item.UsuarioNombre == usuario) && item.UsuarioContrasenia == contrasenia).FirstOrDefault();

       }
       

    }
}
