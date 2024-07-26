using ProyectoFinal_ActivosFijos.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal_ActivosFijos.Models.ViewModel;

namespace ProyectoFinal_ActivosFijos.Controllers
{
    public class AdminController : Controller
    {
        [VerifySession]
        public ActionResult Index()
        {
            var usuarioActual = Session["UsuarioActual"] as UsuariosViewModel;
            return View();
        }      

    }
}