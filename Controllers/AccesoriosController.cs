using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TiendaIphone.Context;
using TiendaIphone.Models;

namespace TiendaIphone.Controllers
{
    [Authorize]
    public class AccesoriosController : Controller
    {
        private readonly TiendaContext _context;

        public AccesoriosController(TiendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<AccesoriosiPhone> ListadoDeAccesorios = _context.Accesorios;
            return View(ListadoDeAccesorios);
        }


        //Metodo Get
        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Guardar()
        {
            return View();
        }
        //Metodo Post
        [HttpPost]
        public IActionResult Guardar(AccesoriosiPhone accesorio)
        {
            if (ModelState.IsValid)
            {
                var accesorios = _context.Accesorios;
                accesorios.Add(accesorio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Editar(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("el ID ingresado es inexistente.");
            }
            else
            {
                //Devolvemos el iphone
                var accesorio = _context.Accesorios.Find(id);
                if (accesorio == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(accesorio);
            }

        }
        [HttpPost]
        public IActionResult Editar(AccesoriosiPhone accesorio)
        {
            if (ModelState.IsValid)
            {

                var accesorios = _context.Accesorios;
                accesorios.Update(accesorio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        public IActionResult Eliminar(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("El ID ingresado es inexistente.");
            }
            else
            {
                //Devolvemos el libro
                var accesorios = _context.Accesorios.Find(id);
                if (accesorios == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(accesorios);
            }

        }

        [HttpPost]
        public IActionResult Eliminar(AccesoriosiPhone accesorio)
        {
            if (ModelState.IsValid)
            {
                var accesorios = _context.Accesorios;
                accesorios.Remove(accesorio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
