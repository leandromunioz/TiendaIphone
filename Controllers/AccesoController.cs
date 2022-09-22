using Microsoft.AspNetCore.Mvc;
using TiendaIphone.Models;
using TiendaIphone.Logica;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace TiendaIphone.Controllers
{
    public class AccesoController : Controller  
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Usuario _user )
        {
            //Traemos la informacion de la "BBDD"

            LO_Usuario InfoUsuario = new LO_Usuario();
            //Validamos la informacion

            var usuario = InfoUsuario.ValidarUsuario(_user.UsuarioEmail, _user.UsuarioContrasenia);
     

            //Si el usuario existe redirige a home

            if (usuario != null)
            {
                //Claims
                var claims = new List<Claim>
                {

                    new Claim(ClaimTypes.Name, usuario.UsuarioNombre),
                    new Claim("Correo", usuario.UsuarioEmail) 
                };

                //Se iteran los roles y se almacenan en los claims
                foreach (string rol in usuario.UsuarioRol)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
                //metemos toda la info en una variable
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            
                //Metodo primero, controlador despues
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }
        //Salir del home de vuelta ala vista login
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acceso");
        }
    }
}
