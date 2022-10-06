using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TiendaIphone.Context;
using TiendaIphone.Models;

namespace TiendaIphone.Controllers
{
    //Etiqueta que autoriza globalmente el acceso a todos los métodos sólo si el usuario está autenticado.
    [Authorize]
    public class AccesoriosController : Controller
    {
        private readonly TiendaContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public AccesoriosController(TiendaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método que muestra el listado de accesorios en stock
        /// </summary>
        /// <returns> Devuelve a la vista el listado de accesorios</returns>
        public async Task <IActionResult> Index()
        {
            IEnumerable<AccesoriosiPhone> ListadoDeAccesorios = await _context.Accesorios.ToListAsync();
            return View(ListadoDeAccesorios);
        }

        /// <summary>
        /// Método que muestra el listado de accesorios vendidos
        /// </summary>
        /// <returns>Devuelve a la vista el listado de accesorios vendidos</returns>
        public async Task <IActionResult> Vendidos()
        {
            IEnumerable<AccesoriosiPhone> ListadoDeAccesorios = await _context.Accesorios.ToListAsync();
            return View(ListadoDeAccesorios);

        }



        /// <summary>
        /// Método que muestra los datos cargados y los devuelve.
        /// </summary>
        /// <returns>Devuelve la vista del método</returns>
        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Guardar()
        {
            return View();
        }


       /// <summary>
       /// Método que carga los datos de los productos al sistema y añade el objeto con sus datos cargados
       /// </summary>
       /// <param name="accesorio"></param>
       /// <returns></returns>
        [HttpPost]
        public async Task <IActionResult> Guardar(AccesoriosiPhone accesorio)
        {
            if (ModelState.IsValid)
            {
                var accesorios = _context.Accesorios;
                accesorio.DisponibilidadAccesorio = Disponibilidad.Disponible;
                accesorio.FechaAlta = DateTime.Now;
                await accesorios.AddAsync(accesorio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }


        /// <summary>
        /// Método que captura el id que se le envía y si es correcto te devuelve la vista con ese objeto, y además verifica el nivel de autorización del usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns>devuelve la vista con el nombre del objeto encontrado</returns>
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

        /// <summary>
        /// Método encargado de editar los valores del objeto y subirlos al sistema
        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns>devuelve la vista del index</returns>
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

        /// <summary>
        /// Método encargado de eliminar un objeto del sistema
        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns>Devuelve a la vista del index con el listado actualizado</returns>
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

        /// <summary>
        ///  METODO PARA VENDER UN ACCESORIO. CAMBIA EL ESTADO A AGOTADO Y LO MUEVE DE SU VISTA DE INICIO A LA VISTA DE VENTAS.

        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns></returns>

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

        /// <summary>
        /// Método encargado de recuperar el artículo, haciendo la acción inversa del método Vender
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
