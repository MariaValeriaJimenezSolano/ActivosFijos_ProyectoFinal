using ProyectoFinal_ActivosFijos.Filters;
using ProyectoFinal_ActivosFijos.Models;
using ProyectoFinal_ActivosFijos.Models.TableViewModel;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_ActivosFijos.Controllers
{
    [VerifySession]
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            List<UsuariosTableViewModel> lstUsuarios = new List<UsuariosTableViewModel>();

            using (ActivosFijosBDEntities db = new ActivosFijosBDEntities())
            {
                var query = from u in db.Usuarios
                            select new UsuariosTableViewModel
                            {
                                Id = u.Id,
                                Nombre = u.Nombre,
                                Cedula = u.Cedula,
                                PrimerApellido = u.PrimerApellido,
                                SegundoApellido = u.SegundoApellido,
                                Edad = (int)u.Edad,
                                Telefono = (int)u.Telefono,
                                Correo = u.Correo,
                                Direccion = u.Direccion,
                                TipoDeUsuario = (int)u.TipoDeUsuario,
                                Contrasena = u.Contrasena,
                                Sexo = u.Sexo,

                            };
                lstUsuarios = query.ToList();
            }
            return View(lstUsuarios);
        }

        //AGREGAR
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UsuariosTableViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            using (var db = new ActivosFijosBDEntities())
            {
                Usuarios usuariosTO = new Usuarios
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Cedula = model.Cedula,
                    PrimerApellido = model.PrimerApellido,
                    SegundoApellido = model.SegundoApellido,
                    Edad = model.Edad,
                    Telefono = model.Telefono,
                    Correo = model.Correo,
                    Direccion = model.Direccion,
                    Sexo = model.Sexo,
                    TipoDeUsuario = model.TipoDeUsuario,
                    Contrasena = model.Contrasena,
                };

                db.Usuarios.Add(usuariosTO);
                db.SaveChanges();

                return Redirect(Url.Content("~/Usuario/Index"));
            }
        }

        //EDITAR
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            using (var db = new ActivosFijosBDEntities())
            {
                var usuario = db.Usuarios.Find(Id);

                if (usuario == null)
                {
                    return RedirectToAction("Index");
                }

                var model = new UsuariosTableViewModel
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Cedula = usuario.Cedula,
                    PrimerApellido = usuario.PrimerApellido,
                    SegundoApellido = usuario.SegundoApellido,
                    Edad = (int)usuario.Edad,
                    Telefono = (int)usuario.Telefono,
                    Correo = usuario.Correo,
                    Direccion = usuario.Direccion,
                    TipoDeUsuario = (int)usuario.TipoDeUsuario,
                    Contrasena = usuario.Contrasena,
                    Sexo = usuario.Sexo,

                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(UsuariosTableViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            using (var db = new ActivosFijosBDEntities())
            {
                var usuarioTO = db.Usuarios.Find(model.Id);

                usuarioTO.Nombre = model.Nombre;
                usuarioTO.Cedula = model.Cedula;
                usuarioTO.PrimerApellido = model.PrimerApellido;
                usuarioTO.SegundoApellido = model.SegundoApellido;
                usuarioTO.Edad = model.Edad;
                usuarioTO.Telefono = model.Telefono;
                usuarioTO.Correo = model.Correo;
                usuarioTO.Direccion = model.Direccion;
                usuarioTO.TipoDeUsuario = model.TipoDeUsuario;
                usuarioTO.Contrasena = model.Contrasena;
                usuarioTO.Sexo = model.Sexo;

                db.SaveChanges();

                return Redirect(Url.Content("~/Usuario/Index"));
            }
        }

        //BORRAR        
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            using (var db = new ActivosFijosBDEntities())
            {
                var usuarioTO = db.Usuarios.Find(Id);

                if (usuarioTO == null)
                {
                    return HttpNotFound();
                }

                db.Usuarios.Remove(usuarioTO);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}

