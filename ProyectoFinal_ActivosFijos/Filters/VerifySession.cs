using ProyectoFinal_ActivosFijos.Controllers;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_ActivosFijos.Filters
{
    public class VerifySession : ActionFilterAttribute
    {  
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Verifica si el usuario está autenticado
            var usuarioActual = filterContext.HttpContext.Session["UsuarioActual"] as UsuariosViewModel;

            // Si el usuario no está autenticado y no está en la página de login, redirige a la página de login
            if (usuarioActual == null && !(filterContext.Controller is LoginController) && !(filterContext.Controller is RegisterController))
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
                return;
            }

            if (usuarioActual != null)
            {
                string controlador = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string accion = filterContext.ActionDescriptor.ActionName;

                //Administrador
                if (usuarioActual.TipoDeUsuario == 1)
                {
                    if ((controlador == "Comprador" || controlador == "Carrito") ||
                        (controlador == "Carros" && (accion.Contains("VistaVehiculos") || accion.Contains("MostrarVehiculoIndividual"))) ||
                        (controlador == "Repuesto" && (accion.Contains("VistaRepuestos") || accion.Contains("MostrarRepuestoIndividual"))))
                    {
                        filterContext.Result = new RedirectResult("~/Admin/AccesoBloqueado");
                        return;
                    }
                }
                //Cliente
                else if (usuarioActual.TipoDeUsuario == 2)
                {                    
                    if ((controlador == "Admin" || controlador == "Usuario") ||
                        (controlador == "Carros" && !(accion.Contains("VistaVehiculos") || accion.Contains("MostrarVehiculoIndividual"))) ||
                        (controlador == "Repuesto" && !(accion.Contains("VistaRepuestos") || accion.Contains("MostrarRepuestoIndividual"))))
                    {
                        filterContext.Result = new RedirectResult("~/Comprador/AccesoBloqueado");
                        return;
                    }
                }               
                else
                {
                    
                }
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
