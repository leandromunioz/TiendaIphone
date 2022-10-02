using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TiendaIphone.Context;
using TiendaIphone.Models;
namespace TiendaIphone.Controllers
{
    [Authorize]
    public class TiendaController : Controller
    {

        private readonly TiendaContext _context;

        public TiendaController(TiendaContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "SuperAdmin")]


        //Metodo Post

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(Tienda tienda1)
        {
            if (ModelState.IsValid)
            {
                var tiendas = _context.Tienda;
                foreach (Tienda tienda in tiendas)
                {
                    if (tienda.ID == tienda1.ID)
                    {
                        return RedirectToAction("Index");

                    }
                }
                

                if (tienda1.NombreTienda is not null )
                {
                    tiendas.Add(tienda1);

                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Iphone");
            }
            return View();
        }
    }
}
