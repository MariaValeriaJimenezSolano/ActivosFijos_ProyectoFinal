using ProyectoFinal_ActivosFijos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using ProyectoFinal_ActivosFijos.Filters;

namespace ProyectoFinal_ActivosFijos.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {            
            using (ActivosFijosBDEntities db = new ActivosFijosBDEntities())
            {
                var user = db.Database.SqlQuery<UsuariosViewModel>("EXEC VerificarUsuario @var_cedula, @var_contrasena",
                    new SqlParameter("var_cedula", username),
                    new SqlParameter("var_contrasena", password)
                ).SingleOrDefault();


                SqlParameter outputParam = new SqlParameter
                {
                    ParameterName = "@Autenticado",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };
                var user2 = db.Database.SqlQuery<UsuariosViewModel>("EXEC VerifyPassword @Cedula, @Contrasena, @Autenticado OUTPUT",
                    new SqlParameter("Cedula", username),
                    new SqlParameter("Contrasena", password),
                    outputParam
                ).SingleOrDefault();
                               
                if (user != null)
                {                    
                    if (user.TipoDeUsuario == 1)
                    {
                        Session["UsuarioActual"] = user;
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (user.TipoDeUsuario == 2)  
                    {
                        Session["UsuarioActual"] = user;
                        return RedirectToAction("Index", "Comprador");
                    }
                }
                else if (outputParam.Value != DBNull.Value)
                {
                    bool resultadoAutenticado;
                    if (outputParam.Value is bool)
                    {
                        resultadoAutenticado = (bool)outputParam.Value;
                    }
                    else if (outputParam.Value is int)
                    {
                        resultadoAutenticado = (int)outputParam.Value != 0;
                    }
                    else
                    {
                        resultadoAutenticado = false;
                    }

                    if (!resultadoAutenticado)
                    {
                        ViewBag.ErrorMessage = "La contraseña es incorrecta";
                        return View("Index");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Usuario no encontrado, favor registrarse";
                    return View("Index");
                }

            }
            return View("Index");
        }

    }
}