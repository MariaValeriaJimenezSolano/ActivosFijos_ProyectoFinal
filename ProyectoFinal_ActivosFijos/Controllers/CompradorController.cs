using ProyectoFinal_ActivosFijos.Filters;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using ProyectoFinal_ActivosFijos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal_ActivosFijos.Models.TableViewModel;

namespace ProyectoFinal_ActivosFijos.Controllers
{
    public class CompradorController : Controller
    {
        [VerifySession]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [VerifySession]
        public ActionResult Edit(int id)
        {
            using (var db = new ActivosFijosBDEntities())
            {
                var usuario = db.Usuarios.Find(id);
                if (usuario == null)
                {
                    return RedirectToAction("Index");
                }
                var model = new UsuariosViewModel
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
                    Sexo = usuario.Sexo,
                    ProductosEnCarrito = usuario.ProductosEnCarrito
                };
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerifySession]
        public ActionResult Edit(UsuariosViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            using (var db = new ActivosFijosBDEntities())
            {
                var usuario = db.Usuarios.Find(model.Id);

                if (usuario != null)
                {
                    usuario.Nombre = model.Nombre;
                    usuario.Cedula = model.Cedula;
                    usuario.PrimerApellido = model.PrimerApellido;
                    usuario.SegundoApellido = model.SegundoApellido;
                    usuario.Edad = model.Edad;
                    usuario.Telefono = model.Telefono;
                    usuario.Correo = model.Correo;
                    usuario.Direccion = model.Direccion;
                    usuario.Sexo = model.Sexo;

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        [VerifySession]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult AccesoBloqueado()
        {
            return View();
        }

    }
}