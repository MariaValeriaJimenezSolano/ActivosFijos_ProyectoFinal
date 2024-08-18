using ProyectoFinal_ActivosFijos.Filters;
using ProyectoFinal_ActivosFijos.Models;
using ProyectoFinal_ActivosFijos.Models.TableViewModel;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
namespace ProyectoFinal_ActivosFijos.Controllers
{
    public class RepuestoController : Controller
    {
        [VerifySession]
        public ActionResult Index(string estado, string modelo, string descripcion, string nombre, string marca)
        {
            var usuarioActual = Session["UsuarioActual"] as UsuariosViewModel;

            List<RepuestosTableViewModel> lstRepuestos = new List<RepuestosTableViewModel>();

            using (ActivosFijosBDEntities db = new ActivosFijosBDEntities())
            {
                var query = from r in db.Repuestos
                            select new RepuestosTableViewModel
                            {

                                Id = r.Id,
                                Nombre = r.Nombre,
                                Estado = r.Estado,
                                Marca = r.Marca,
                                Modelo = r.Modelo,
                                Anio = (int)r.Anio,
                                Descripcion = r.Descripcion,
                                Precio = (decimal)r.Precio,
                                CantidadEnStock = (int)r.CantidadEnStock,
                                Imagen1 = r.Imagen1,
                                Imagen2 = r.Imagen2,
                            };

                // Apply filters
                if (!string.IsNullOrEmpty(marca))
                {
                    query = query.Where(r => r.Marca.Contains(marca));
                }

                if (!string.IsNullOrEmpty(modelo))
                {
                    query = query.Where(r => r.Modelo.Contains(modelo));
                }

                if (!string.IsNullOrEmpty(estado))
                {
                    query = query.Where(r => r.Estado.Contains(estado));
                }

                lstRepuestos = query.ToList();
            }

            return View(lstRepuestos);

        }


        //AGREGAR
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(RepuestosTableViewModel model, HttpPostedFileBase ImagenFile, HttpPostedFileBase ImagenFile2)
        {
            if (!ModelState.IsValid) return View(model);

            using (var db = new ActivosFijosBDEntities())
            {
                byte[] imagenBytes = null;
                byte[] imagenBytes2 = null;

                if (ImagenFile != null && ImagenFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(ImagenFile.InputStream))
                    {
                        imagenBytes = binaryReader.ReadBytes(ImagenFile.ContentLength);
                    }
                }

                if (ImagenFile2 != null && ImagenFile2.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(ImagenFile2.InputStream))
                    {
                        imagenBytes2 = binaryReader.ReadBytes(ImagenFile2.ContentLength);
                    }
                }

                Repuesto repuestosTO = new Repuesto
                {
                    // Id = model.Id,
                    Nombre = model.Nombre,
                    Estado = model.Estado,
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    Anio = (int)model.Anio,
                    Descripcion = model.Descripcion,
                    Precio = (decimal)model.Precio,
                    CantidadEnStock = (int)model.CantidadEnStock,
                    Imagen1 = imagenBytes,
                    Imagen2 = imagenBytes2,
                };

                try
                {
                    db.Repuestos.Add(repuestosTO);
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Repuesto/Index"));
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    // Captura detalles de la excepción interna
                    var innerException = ex.InnerException?.InnerException;
                    if (innerException != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error interno: {innerException.Message}");
                    }

                    // Opción para capturar y mostrar detalles de la excepción
                    ModelState.AddModelError("", "Ocurrió un error al intentar guardar los datos. Por favor, intenta nuevamente.");

                    // Retorna la vista con el modelo
                    return View(model);
                }
            }
        }


        //EDITAR
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            using (var db = new ActivosFijosBDEntities())
            {
                var repuesto = db.Repuestos.Find(Id);

                //ViewBag.repuestoNombre = repuesto.Nombre; // SALVAMOS EL NOMBRE DEL repuesto EN UN VIEWBAG PARA USARLO LUEGO EN LA VISTA DE EDIT

                if (repuesto == null)
                {
                    return RedirectToAction("Index");
                }

                var model = new RepuestosTableViewModel
                {
                    Id = repuesto.Id,
                    Nombre = repuesto.Nombre,
                    Estado = repuesto.Estado,
                    Descripcion = repuesto.Descripcion,
                    Precio = (decimal)repuesto.Precio,
                    Modelo = repuesto.Modelo,
                    Marca = repuesto.Marca,
                    Anio = (int)repuesto.Anio,
                    CantidadEnStock = (int)repuesto.CantidadEnStock,
                    Imagen1 = repuesto.Imagen1,
                    Imagen2 = repuesto.Imagen2,

                };

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(RepuestosTableViewModel model, HttpPostedFileBase NuevaImagen, HttpPostedFileBase NuevaImagen2, HttpPostedFileBase NuevaImagen3, string action) // INCLUIDOS LOS PARAMETROS PARA LAS IMAGENES  llaamdsod nuevaimagen# eso le dice cual debe controlar
        {
            if (!ModelState.IsValid) return View(model);

            using (var db = new ActivosFijosBDEntities())
            {
                var repuestoTO = db.Repuestos.Find(model.Id);

                repuestoTO.Id = model.Id;
                repuestoTO.Nombre = model.Nombre;
                repuestoTO.Descripcion = model.Descripcion;
                repuestoTO.Precio = model.Precio;
                repuestoTO.Modelo = model.Modelo;
                repuestoTO.Estado = model.Estado;
                repuestoTO.Anio = model.Anio;
                repuestoTO.CantidadEnStock = model.CantidadEnStock;

                if (NuevaImagen != null && NuevaImagen.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(NuevaImagen.InputStream))
                    {
                        repuestoTO.Imagen1 = binaryReader.ReadBytes(NuevaImagen.ContentLength);
                    }
                }

                if (NuevaImagen2 != null && NuevaImagen2.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(NuevaImagen2.InputStream))
                    {
                        repuestoTO.Imagen2 = binaryReader.ReadBytes(NuevaImagen2.ContentLength);
                    }
                }

                if (action == "EliminarImagen2" || model.EliminarImagen2)
                {
                    repuestoTO.Imagen2 = null;

                }

                else if (NuevaImagen2 != null && NuevaImagen2.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(NuevaImagen2.InputStream))
                    {
                        repuestoTO.Imagen2 = binaryReader.ReadBytes(NuevaImagen2.ContentLength);
                    }
                }

                db.SaveChanges();

                return Redirect(Url.Content("~/Repuesto/Index"));
            }
        }

        //BORRAR
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            try
            {
                using (var db = new ActivosFijosBDEntities())
                {
                    var repuesto = Session["RepuestoActual"] as RepuestosViewModel;

                    var repuestoTO = db.Repuestos.Find(Id);

                    if (repuestoTO == null)
                    {
                        return HttpNotFound();
                    }

                    db.Repuestos.Remove(repuestoTO);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al eliminar el usuario.");
            }
            return RedirectToAction("Index");
        }




        [HttpGet]
        public ActionResult VistaRepuestos(string nombre, string modelo, decimal? precioMin, decimal? precioMax)
        {
            var usuarioActual = Session["UsuarioActual"] as UsuariosViewModel;
            List<RepuestosTableViewModel> lstRepuestos = new List<RepuestosTableViewModel>();

            using (ActivosFijosBDEntities db = new ActivosFijosBDEntities())
            {
                var query = from r in db.Repuestos
                            select new RepuestosTableViewModel
                            {
                                Id = r.Id,
                                Nombre = r.Nombre,
                                Descripcion = r.Descripcion,
                                Precio = (decimal)r.Precio,
                                Estado = r.Estado,
                                Anio = (int)r.Anio,
                                CantidadEnStock = (int)r.CantidadEnStock,
                                Modelo = r.Modelo,
                                Marca = r.Marca,
                                Imagen1 = r.Imagen1,
                                Imagen2 = r.Imagen2
                            };

                if (!string.IsNullOrEmpty(modelo))
                {
                    query = query.Where(r => r.Modelo.Contains(modelo));
                }

                if (!string.IsNullOrEmpty(nombre))
                {
                    query = query.Where(r => r.Nombre.Contains(nombre));
                }

                if (precioMin.HasValue)
                {
                    query = query.Where(r => r.Precio >= precioMin.Value);
                }

                if (precioMax.HasValue)
                {
                    query = query.Where(r => r.Precio <= precioMax.Value);
                }

                ViewBag.ModeloSeleccionado = modelo;
                ViewBag.NombreSeleccionado = nombre;
                ViewBag.PrecioMaxSeleccionado = precioMax ?? 1000000;

                lstRepuestos = query.ToList();
            }

            return View(lstRepuestos);
        }


      public ActionResult MostrarRepuestoIndividual(int Id)
      {
            RepuestosViewModel model = new RepuestosViewModel();
            using (var db = new ActivosFijosBDEntities())
            {
                var repuestoTO = db.Repuestos.Find(Id);

                model.Id = repuestoTO.Id;
                model.Marca = repuestoTO.Marca;
                model.Modelo = repuestoTO.Modelo;
                model.Anio = repuestoTO.Anio;
                model.Precio = repuestoTO.Precio;
                model.Nombre = repuestoTO.Nombre;
                model.Estado = repuestoTO.Estado;
                model.CantidadEnStock = repuestoTO.CantidadEnStock;
                model.Descripcion = repuestoTO.Descripcion;
                model.Imagen1 = repuestoTO.Imagen1;
                model.Imagen2 = repuestoTO.Imagen2;
            }
            return View(model);
        }

    }
}

