using ProyectoFinal_ActivosFijos.Models;
using ProyectoFinal_ActivosFijos.Models.TableViewModel;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_ActivosFijos.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
       /* public ActionResult Index()
        {
       hola
       adios parte 3 
       no se pudoasdfasdfasdfasdfsadfasdfa
       Ya me cansefasdfasdf            return View();
        }
       */
        public ActionResult Index(string precio)
        {
            List<CarrosTableViewModel> lstProductos = new List<CarrosTableViewModel>();

            using (ActivosFijosBDEntities db = new ActivosFijosBDEntities())
            {
                var query = from c in db.Carros
                            select new CarrosTableViewModel
                            {
                                Id = c.Id,
                                Marca = c.Marca,
                                Modelo = c.Modelo,
                                Anio = (int)c.Anio,
                                Precio = (decimal)c.Precio,
                                Transmision = c.Transmision,
                                Combustible = c.Combustible,
                                CantidadEnStock = (int)c.CantidadEnStock,
                                Descripcion = c.Descripcion,
                                Imagen1 = c.Imagen1,
                                Imagen2 = c.Imagen2
                            };                            
                lstProductos = query.ToList();
            }

            return View(lstProductos);

        }


    }
}