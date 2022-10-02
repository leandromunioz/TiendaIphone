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

        public IActionResult Vendidos()
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
                accesorio.DisponibilidadAccesorio = Disponibilidad.Disponible;
                accesorio.FechaAlta = DateTime.Now;
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
        public IActionResult Eliminar(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("El ID ingresado es inexistente.");
            }
            else
            {
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

        //METODO PARA VENDER UN ACCESORIO. CAMBIA EL ESTADO A AGOTADO Y LO MUEVE DE SU VISTA DE INICIO A LA VISTA DE VENTAS.


        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Vender(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("El ID ingresado es inexistente.");
            }
            else
            {
                var accesorios = _context.Accesorios.Find(id);
                if (accesorios == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(accesorios);
            }

        }

        [HttpPost]
        public IActionResult Vender(AccesoriosiPhone accesorio)
        {
            if (ModelState.IsValid)
            {
                var accesorios = _context.Accesorios;
                accesorio = accesorios.Find(accesorio.AccesorioID);
                accesorio.DisponibilidadAccesorio = Disponibilidad.Agotado;
                accesorio.FechaAlta = DateTime.Now;
                _context.Accesorios.Update(accesorio);
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
                var accesorios = _context.Accesorios.Find(id);
                if (accesorios == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(accesorios);
            }

        }

        [HttpPost]
        public IActionResult Recuperar (AccesoriosiPhone accesorio)
        {
            if (ModelState.IsValid)
            {
                var accesorios = _context.Accesorios;
                accesorio = accesorios.Find(accesorio.AccesorioID);
                accesorio.DisponibilidadAccesorio = Disponibilidad.Disponible;
                accesorio.FechaAlta = DateTime.Now;
                _context.Accesorios.Update(accesorio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
