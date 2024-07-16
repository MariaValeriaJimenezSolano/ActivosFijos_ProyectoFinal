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
    public class UsuarioController : Controller
    {
        // GET: Usuario

        //Aca tengo la duda de que tengo que poner en los parametros y por que 

        public ActionResult Inuex()
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
                                TipoDeUsuario = (int)u.TipoDeUsuario,
                                Contrasena = u.Contrasena,

                            };

                lstUsuarios = query.ToList();
            }

            return View(lstUsuarios);

        }


        // --------------------------------------------  ADD  -----------------------------------------------

        [HttpGet]

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        // Aqui estamos pasando los parametros del form en ADD donde estan los productos model y algo especial para las imagenes 1 2 y 3
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
                    TipoDeUsuario = model.TipoDeUsuario,
                    Contrasena = model.Contrasena,
                };

                db.Usuarios.Add(usuariosTO);
                db.SaveChanges();

                return Redirect(Url.Content("~/Usuario/Index"));
            }
        }



        // --------------------------------------------  EDIT  -----------------------------------------------

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
                    TipoDeUsuario = (int)usuario.TipoDeUsuario,
                    Contrasena = usuario.Contrasena,
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
                usuarioTO.TipoDeUsuario = model.TipoDeUsuario;
                usuarioTO.Contrasena = model.Contrasena;                
               
                db.SaveChanges();

                return Redirect(Url.Content("~/Usuario/Index"));
            }
        }



        // --------------------------------------------  DELETE  -----------------------------------------------



        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new ActivosFijosBDEntities())
            {
                var usuarioTO = db.Usuarios.Find(id);

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
