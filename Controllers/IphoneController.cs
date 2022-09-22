using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TiendaIphone.Context;
using TiendaIphone.Models;

namespace TiendaIphone.Controllers
{
    public class IphoneController : Controller
    {
        private readonly TiendaContext _context;

        public IphoneController(TiendaContext context)
        {
            _context = context; 
        }

        public IActionResult Index()
        {
            IEnumerable<Iphone> ListadoDeIphone = _context.Iphones;
            return View(ListadoDeIphone);
        }


        //Metodo Get
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Guardar()
        {
            return View();
        }
        //Metodo Post
        [HttpPost]
        public IActionResult Guardar(Iphone iphone)
        {
            if (ModelState.IsValid)
            {
                var iphones = _context.Iphones;
                iphones.Add(iphone);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Editar(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("el ID ingresado es inexistente.");
            }
            else
            {
                //Devolvemos el iphone
                var iphone = _context.Iphones.Find(id);
                if (iphone == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(iphone);
            }

        }
        [HttpPost]
        public IActionResult Editar(Iphone iphone)
        {
            if (ModelState.IsValid)
            {

                var iphones = _context.Iphones;
                iphones.Update(iphone);
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
                var iphones = _context.Iphones.Find(id);
                if (iphones == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(iphones);
            }

        }

        [HttpPost]
        public IActionResult Eliminar(Iphone iphone)
        {
            if (ModelState.IsValid)
            {
                var libros = _context.Iphones;
                libros.Remove(iphone);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }







    }
}
