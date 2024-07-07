using ProyectoFinal_ActivosFijos.Models;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_ActivosFijos.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UsuariosViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (usuarioExisteCedula(model.Cedula) == 1)
                {
                    TempData["Mensaje"] = "Cedula ya esta registrado en el sistema, por favor intente de nuevo";
                }
                else
                {
                    using (var db = new ActivosFijosBDEntities())
                    {
                        Usuarios userTO = new Usuarios();
                        userTO.Cedula = model.Cedula;
                        userTO.Contrasena = model.Contrasena;
                        userTO.Correo = model.Correo;
                        userTO.Nombre = model.Nombre;
                        userTO.PrimerApellido = model.PrimerApellido;
                        userTO.SegundoApellido = model.SegundoApellido;
                        userTO.Edad = model.Edad;
                        userTO.Telefono = model.Telefono;
                        userTO.Sexo = model.Sexo;
                        userTO.Direccion = model.Direccion;
                        userTO.TipoDeUsuario = 2; //Comprador
                        db.Usuarios.Add(userTO);
                        db.SaveChanges();

                    }
                    return Redirect("/Login/Index");
                }
            }
            return View(model);
        }

        public int usuarioExisteCedula(int cedula)
        {
            using (ActivosFijosBDEntities db = new ActivosFijosBDEntities())
            {
                var user = db.Database.SqlQuery<UsuariosViewModel>("EXEC VerifyUserCedula @var_cedula",
                    new SqlParameter("var_cedula", cedula)
                ).SingleOrDefault();

                SqlParameter outputParam = new SqlParameter
                {
                    ParameterName = "@Autenticado",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                if (user != null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }


    }
}