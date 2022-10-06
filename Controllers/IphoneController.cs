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
    public class IphoneController : Controller
    {
        private readonly TiendaContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public IphoneController(TiendaContext context)
        {
            _context = context; 
        }

        /// <summary>
        /// Método que muestra el listado de accesorios en stock
        /// </summary>
        /// <returns> Devuelve a la vista el listado de accesorios</returns>
        public async Task <IActionResult> Index()
        {
            IEnumerable<Iphone> ListadoDeIphone = await _context.Iphones.ToListAsync();
            return View(ListadoDeIphone);

        }

        /// <summary>
        /// Método que muestra el listado de accesorios vendidos
        /// </summary>
        /// <returns>Devuelve a la vista el listado de accesorios vendidos</returns>
        public async Task <IActionResult>  Vendidos()
        {
            IEnumerable<Iphone> ListadoDeIphone = await _context.Iphones.ToListAsync();
            return View(ListadoDeIphone);

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
                //Devolvemos el iphone
                var iphone = _context.Iphones.Find(id);
                if (iphone == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(iphone);
            }

        }


        /// <summary>
        /// Método encargado de editar los valores del objeto y subirlos al sistema
        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns>devuelve la vista del index</returns>
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

        /// <summary>
        /// Método encargado de eliminar un objeto del sistema
        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns>Devuelve a la vista del index con el listado actualizado</returns>
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


        /// <summary>
        ///  METODO PARA VENDER UN iPhone. CAMBIA EL ESTADO A AGOTADO Y LO MUEVE DE SU VISTA DE INICIO A LA VISTA DE VENTAS.

        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns></returns>

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

        /// <summary>
        /// Método encargado de recuperar el artículo, haciendo la acción inversa del método Vender
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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
