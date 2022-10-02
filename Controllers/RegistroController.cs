using Microsoft.AspNetCore.Mvc;
using TiendaIphone.Models;
using TiendaIphone.Context;

namespace TiendaIphone.Controllers
{
    public class RegistroController : Controller
    {
        private readonly TiendaContext _context;

        public RegistroController(TiendaContext context)
        {
            _context = context; 
        }

        public IActionResult Index()
        {
            return View();
        }


        //Metodo Post

        public IActionResult Guardar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Guardar(Usuario usuario1)
        {
            if (ModelState.IsValid)
            {
                var usuarios = _context.Usuarios;
                foreach (Usuario usuario in usuarios )
                {
                   if (usuario.UsuarioEmail == usuario1.UsuarioEmail)
                    {
                        return RedirectToAction("Index");

                    }
                }
                if (usuarios.Count() == 0)
                {
                    usuario1.UsuarioRol = "SuperAdmin";

                }
                else
                {
                    usuario1.UsuarioRol = "admin";
                }

                if (usuario1.UsuarioEmail is not null && usuario1.UsuarioNombre is not null && usuario1.UsuarioContrasenia is not null)
                {
                    usuarios.Add(usuario1);

                }
                _context.SaveChanges();
                return RedirectToAction("Index" , "Acceso");
            }
            return View();
        }
    }
}
