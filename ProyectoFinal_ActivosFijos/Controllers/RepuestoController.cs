using ProyectoFinal_ActivosFijos.Models;
using ProyectoFinal_ActivosFijos.Models.TableViewModel;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
namespace ProyectoFinal_ActivosFijos.Controllers
{
    public class RepuestoController : Controller
    {
        // GET: Repuesto



        public ActionResult Index(string estado, string modelo,  string descripcion, string nombre,string marca)
        {

            //var usuarioActual = Session["UsuarioActual"] as RepuestosViewModel;

            //ViewBag.UsuarioActual = usuarioActual.Nombre;
            //ViewBag.SexoUsuario = usuarioActual.Sexo;
            //ViewBag.TipoUsuario = usuarioActual.TipoDeUsuario;

            List<RepuestosTableViewModel> lstRepuestos = new List<RepuestosTableViewModel>();

            using (ActivosFijosBDEntities db = new ActivosFijosBDEntities())
            {
                var query = from r in db.Repuestos
                            select new RepuestosTableViewModel { 
                            
                                Id = r.Id,
                                Nombre = r.Nombre,
                                Estado = r.Estado,
                                Marca = r.Marca,
                                Modelo = r.Modelo,
                                Anio= (int)r.Anio,
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


        // --------------------------------------------  ADD  -----------------------------------------------

        [HttpGet]

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
       
                               // Aqui estamos pasando los parametros del form en ADD donde estan los repuestos model y algo especial para las imagenes 1 2 y 3
        public ActionResult Add(RepuestosTableViewModel model, HttpPostedFileBase ImagenFile, HttpPostedFileBase ImagenFile2)


        {
            if (!ModelState.IsValid) return View(model);

            using (var db = new ActivosFijosBDEntities())

            {
                // estamos tomando la sesion activa de usuario para instanciarla y manipularla aqui 
                var repuesto = Session["RepuestoActual"] as RepuestosViewModel;


                // capturando la imagen que se subio como un null por ahora , ya luego cuando el user la suba habra una conversion a base 64
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



                // fin de proceso de captura de imagenes 

                Repuestos repuestosTO = new Repuestos
                {
                   

                    Id = model.Id,
                    Nombre = model.Nombre,
                    Estado =model.Estado,
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    Anio = (int)model.Anio,
                    Descripcion = model.Descripcion,
                    Precio = (decimal)model.Precio,
                    CantidadEnStock = (int)model.CantidadEnStock,
                    // Agregar el campo de imagen
                    Imagen1 = model.Imagen1,
                    Imagen2 = model.Imagen2,
                
                  

                };

                db.Repuestos.Add(repuestosTO);
                db.SaveChanges();

                return Redirect(Url.Content("~/Repuesto/Index"));
            }
        }
        // --------------------------------------------  EDIT  -----------------------------------------------

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            using (var db = new ActivosFijosBDEntities())
            {
                var repuesto = db.Repuestos.Find(Id); // El ProductID viene como parametro para poder buscar el repuesto

                ViewBag.repuestoNombre = repuesto.Nombre; // SALVAMOS EL NOMBRE DEL repuesto EN UN VIEWBAG PARA USARLO LUEGO EN LA VISTA DE EDIT

                //ViewBag.repuestoGenero = repuesto.Lugar;


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
                    Marca = repuesto.Marca,
                    Anio = (int)repuesto.Anio,
                    CantidadEnStock = (int)repuesto.CantidadEnStock,
                    // Cargando las imagenes para que la persona vea las que tiene relacionadas a ese repuesto 
                    Imagen1 = repuesto.Imagen1,
                    Imagen2 = repuesto.Imagen2,
                  
                };

                //// Esta pequeña validacion de abajo lo que hace es que recuerde cual es el genero que ya tenia el repuesto y lo muestre si la persona queire cambiarlo puede hacerlo 
                //ViewBag.GeneroSeleccionado = repuesto.Proveedor;

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(RepuestosTableViewModel model, HttpPostedFileBase NuevaImagen, HttpPostedFileBase NuevaImagen2, HttpPostedFileBase NuevaImagen3, string action) // INCLUIDOS LOS PARAMETROS PARA LAS IMAGENES  llaamdsod nuevaimagen# eso le dice cual debe controlar
        {

            if (!ModelState.IsValid) return View(model);


            using (var db = new ActivosFijosBDEntities())
            {

                // DECLARANDO LA SESION ACTIVA AL USUARIO ACTUAL PARA PASAR POR VIEWBAGS
                var usuario = Session["UsuarioActual"] as RepuestosViewModel;


                // DECLARANDO LOS ATRIBUTOS DE repuesto COMO VIEWBAGS
                var repuestoTO = db.Repuestos.Find(model.Id);


                repuestoTO.Id = model.Id;
                repuestoTO.Nombre = model.Nombre;
                repuestoTO.Descripcion = model.Descripcion;
                repuestoTO.Precio = model.Precio;
                repuestoTO.Estado = model.Estado;
                repuestoTO.Anio = model.Anio;
                repuestoTO.CantidadEnStock = model.CantidadEnStock;
                
                // IMAGENES  -- Esto tambien aplica para eliminarlas o agregar otra 

                // Actualiza las propiedades de las imágenes según la acción del usuario
                if (NuevaImagen != null && NuevaImagen.ContentLength > 0)
                {
                    // El usuario ha proporcionado una nueva imagen
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
        // --------------------------------------------  DELETE  -----------------------------------------------


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
    }
}

