using ProyectoFinal_ActivosFijos.Filters;
using ProyectoFinal_ActivosFijos.Models;
using ProyectoFinal_ActivosFijos.Models.TableViewModel;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_ActivosFijos.Controllers
{
    [VerifySession]
    public class CarrosController : Controller
    {      
        public ActionResult Index(string modelo, int? anio, string transmision, string combustible, decimal? precioMin, decimal? precioMax)
        {
            var usuarioActual = Session["UsuarioActual"] as UsuariosViewModel;
            List<CarrosTableViewModel> lstCarros = new List<CarrosTableViewModel>();

            using (ActivosFijosBDEntities db = new ActivosFijosBDEntities())
            {
                var query = from c in db.Carros
                            select new CarrosTableViewModel
                            {
                                Id = c.Id,
                                Marca = c.Marca,
                                Modelo = c.Modelo,
                                Anio = c.Anio ?? 0,
                                Precio = c.Precio ?? 0,
                                Transmision = c.Transmision,
                                Combustible = c.Combustible,
                                CantidadEnStock = c.CantidadEnStock ?? 0,
                                Descripcion = c.Descripcion,
                                Imagen1 = c.Imagen1,
                                Imagen2 = c.Imagen2
                            };


                if (!string.IsNullOrEmpty(modelo))
                {
                    query = query.Where(c => c.Modelo.Contains(modelo));
                }

                if (anio.HasValue)
                {
                    query = query.Where(c => c.Anio == anio.Value);
                }

                if (precioMin.HasValue)
                {
                    query = query.Where(c => c.Precio >= precioMin.Value);
                }

                if (precioMax.HasValue)
                {
                    query = query.Where(c => c.Precio <= precioMax.Value);
                }

                ViewBag.ModeloSeleccionado = modelo;
                ViewBag.AnioSeleccionado = anio;
                ViewBag.PrecioMaxSeleccionado = precioMax ?? 1000000;
                
                lstCarros = query.ToList();
            }
            return View(lstCarros);
        }

        //AGREGAR
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(CarrosTableViewModel model, HttpPostedFileBase ImagenFile, HttpPostedFileBase ImagenFile2)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                using (var db = new ActivosFijosBDEntities())
                {
                    byte[] imagenBytes = null;
                    byte[] imagenBytes2 = null;

                    // Proceso para la primera imagen
                    if (ImagenFile != null && ImagenFile.ContentLength > 0)
                    {
                        using (var binaryReader = new BinaryReader(ImagenFile.InputStream))
                        {
                            imagenBytes = binaryReader.ReadBytes(ImagenFile.ContentLength);
                        }
                    }

                    // Proceso para la segunda imagen
                    if (ImagenFile2 != null && ImagenFile2.ContentLength > 0)
                    {
                        using (var binaryReader = new BinaryReader(ImagenFile2.InputStream))
                        {
                            imagenBytes2 = binaryReader.ReadBytes(ImagenFile2.ContentLength);
                        }
                    }

                    Carros carroTO = new Carros
                    {
                        Marca = model.Marca,
                        Modelo = model.Modelo,
                        Anio = model.Anio,
                        Precio = model.Precio,
                        Transmision = model.Transmision,
                        Combustible = model.Combustible,
                        Descripcion = model.Descripcion,
                        CantidadEnStock = model.CantidadEnStock,
                        Imagen1 = imagenBytes,
                        Imagen2 = imagenBytes2
                    };

                    db.Carros.Add(carroTO);
                    db.SaveChanges();

                    return Redirect(Url.Content("~/Carros/Index"));
                }
            }
        }


        //EDITAR
        [HttpGet]
        public ActionResult Edit(int CarroID)
        {
            using (var db = new ActivosFijosBDEntities())
            {
                var carro = db.Carros.Find(CarroID);

                if (carro == null)
                {
                    return RedirectToAction("Index");
                }

                var model = new CarrosTableViewModel
                {
                    Id = (int)carro.Id,
                    Marca = carro.Marca,
                    Modelo = carro.Modelo,
                    Anio = (int)carro.Anio,
                    Precio = (decimal)carro.Precio,
                    Transmision = carro.Transmision,
                    Combustible = carro.Combustible,
                    CantidadEnStock = (int)carro.CantidadEnStock,
                    Descripcion = carro.Descripcion,
                    Imagen1 = carro.Imagen1,
                    Imagen2 = carro.Imagen2
                };                            

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(CarrosTableViewModel model, HttpPostedFileBase NuevaImagen1, HttpPostedFileBase NuevaImagen2, HttpPostedFileBase NuevaImagen3, string action)
        {            
            if (!ModelState.IsValid) return View(model);

            using (var db = new ActivosFijosBDEntities())
            {                
                var carroTO = db.Carros.Find(model.Id);

                carroTO.Marca = model.Marca;
                carroTO.Modelo = model.Modelo;
                carroTO.Anio = model.Anio;
                carroTO.Precio = model.Precio;
                carroTO.Transmision = model.Transmision;
                carroTO.Combustible = model.Combustible;
                carroTO.CantidadEnStock = model.CantidadEnStock;
                carroTO.Descripcion = model.Descripcion;
                carroTO.Imagen1 = model.Imagen1;
                carroTO.Imagen2 = model.Imagen2;

                if (action == "EliminarImagen1" || model.EliminarImagen1)
                {
                    carroTO.Imagen1 = null;
                }
                else if (NuevaImagen1 != null && NuevaImagen1.ContentLength > 0)
                {                    
                    using (var binaryReader = new BinaryReader(NuevaImagen1.InputStream))
                    {
                        carroTO.Imagen1 = binaryReader.ReadBytes(NuevaImagen1.ContentLength);
                    }
                }

                if (action == "EliminarImagen2" || model.EliminarImagen2)
                {
                    carroTO.Imagen2 = null;
                }
                else if (NuevaImagen2 != null && NuevaImagen2.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(NuevaImagen2.InputStream))
                    {
                        carroTO.Imagen2 = binaryReader.ReadBytes(NuevaImagen2.ContentLength);
                    }
                }

                db.SaveChanges();

                return Redirect(Url.Content("~/Carros/Index"));
            }
        }


        //BORRAR
        [HttpGet]
        public ActionResult Delete(int CarroID)
        {
            using (var db = new ActivosFijosBDEntities())
            {             
                var carroTO = db.Carros.Find(CarroID);

                if (carroTO == null)
                {
                    return HttpNotFound();
                }

                db.Carros.Remove(carroTO);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult VistaVehiculos(string modelo, int? anio, string transmision, string combustible, decimal? precioMin, decimal? precioMax)
        {
            var usuarioActual = Session["UsuarioActual"] as UsuariosViewModel;
            List<CarrosTableViewModel> lstCarros = new List<CarrosTableViewModel>();

            using (ActivosFijosBDEntities db = new ActivosFijosBDEntities())
            {
                var query = from c in db.Carros
                            select new CarrosTableViewModel
                            {
                                Id = c.Id,
                                Marca = c.Marca,
                                Modelo = c.Modelo,
                                Anio = c.Anio ?? 0,
                                Precio = c.Precio ?? 0,
                                Transmision = c.Transmision,
                                Combustible = c.Combustible,
                                CantidadEnStock = c.CantidadEnStock ?? 0,
                                Descripcion = c.Descripcion,
                                Imagen1 = c.Imagen1,
                                Imagen2 = c.Imagen2
                            };


                if (!string.IsNullOrEmpty(modelo))
                {
                    query = query.Where(c => c.Modelo.Contains(modelo));
                }

                if (anio.HasValue)
                {
                    query = query.Where(c => c.Anio == anio.Value);
                }

                if (precioMin.HasValue)
                {
                    query = query.Where(c => c.Precio >= precioMin.Value);
                }

                if (precioMax.HasValue)
                {
                    query = query.Where(c => c.Precio <= precioMax.Value);
                }

                ViewBag.ModeloSeleccionado = modelo;
                ViewBag.AnioSeleccionado = anio;
                ViewBag.PrecioMaxSeleccionado = precioMax ?? 1000000;

                lstCarros = query.ToList();
            }
            return View(lstCarros);
        }

        public ActionResult MostrarVehiculoIndividual(int Id)
        {
           CarrosViewModel model = new CarrosViewModel();
            using (var db = new ActivosFijosBDEntities())
            {
                var carroTO = db.Carros.Find(Id);

                model.Id = carroTO.Id;
                model.Marca = carroTO.Marca;
                model.Modelo = carroTO.Modelo;
                model.Anio = carroTO.Anio;
                model.Precio = carroTO.Precio;
                model.Transmision = carroTO.Transmision;
                model.Combustible = carroTO.Combustible;
                model.CantidadEnStock = carroTO.CantidadEnStock;
                model.Descripcion = carroTO.Descripcion;
                model.Imagen1 = carroTO.Imagen1;
                model.Imagen2 = carroTO.Imagen2;
            }
            return View(model);
        }

    }
}