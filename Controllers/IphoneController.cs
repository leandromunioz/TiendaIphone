using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TiendaIphone.Context;
using TiendaIphone.Models;

namespace TiendaIphone.Controllers
{
    [Authorize]
    public class IphoneController : Controller
    {
        private readonly TiendaContext _context;

        public IphoneController(TiendaContext context)
        {
            _context = context; 
        }

        //Metodo Get

        public IActionResult Index()
        {
            IEnumerable<Iphone> ListadoDeIphone = _context.Iphones;
            return View(ListadoDeIphone);

        }

        public IActionResult Vendidos()
        {
            IEnumerable<Iphone> ListadoDeIphone = _context.Iphones;
            return View(ListadoDeIphone);

        }

        //Metodo Post

        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(Iphone iphone)
        {
            if (ModelState.IsValid)
            {
                var iphones = _context.Iphones;
                iphone.DisponibilidadIphone = DisponibilidadiPhone.Disponible;
                iphone.FechaAltaIphone = DateTime.Now;
                iphones.Add(iphone);
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



        //METODO PARA VENDER UN IPHONE. CAMBIA EL ESTADO A AGOTADO Y LO OCULTA DE LA VISTA.


        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Vender(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("El ID ingresado es inexistente.");
            }
            else
            {
                var iphones = _context.Iphones.Find(id);
                if (iphones == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(iphones);
            }

        }

        [HttpPost]
        public IActionResult Vender(Iphone iphone)
        {
            if (ModelState.IsValid)
            {
                var iphones = _context.Iphones;
                iphone = iphones.Find(iphone.IphoneID);
                iphone.DisponibilidadIphone = DisponibilidadiPhone.Agotado;
                iphone.FechaAltaIphone = DateTime.Now;
                iphones.Update(iphone);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }


        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Recuperar(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("El ID ingresado es inexistente.");
            }
            else
            {
                var iphones = _context.Iphones.Find(id);
                if (iphones == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(iphones);
            }

        }

        [HttpPost]
        public IActionResult Recuperar(Iphone iphone)
        {
            if (ModelState.IsValid)
            {
                var iphones = _context.Iphones;
                iphone = iphones.Find(iphone.IphoneID);
                iphone.DisponibilidadIphone = DisponibilidadiPhone.Disponible;
                iphone.FechaAltaIphone = DateTime.Now;
                iphones.Update(iphone);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }



    }
}
