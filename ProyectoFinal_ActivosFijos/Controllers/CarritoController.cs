using ProyectoFinal_ActivosFijos.Filters;
using ProyectoFinal_ActivosFijos.Models;
using ProyectoFinal_ActivosFijos.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_ActivosFijos.Controllers
{
    [VerifySession]
    public class CarritoController : Controller
    {
        private ActivosFijosBDEntities db = new ActivosFijosBDEntities();

        // GET: Carrito
        public ActionResult Index()
        {
            var usuarioViewModel = Session["UsuarioActual"] as UsuariosViewModel;
            if (usuarioViewModel == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = db.Usuarios.Find(usuarioViewModel.Id);

            var productoIds = usuario.ProductosEnCarrito?.Split(',').Select(p => {
                var parts = p.Split(':');
                return parts.Length == 2 && int.TryParse(parts[1], out var id) ? (int?)id : null;
            }).Where(id => id.HasValue).Select(id => id.Value).ToList() ?? new List<int>();

            var carrosIds = productoIds.Where(p => db.Carros.Any(c => c.Id == p)).ToList();
            var repuestosIds = productoIds.Where(p => db.Repuestos.Any(r => r.Id == p)).ToList();

            ViewBag.Carros = db.Carros.Where(c => carrosIds.Contains(c.Id) && c.CantidadEnStock > 0).ToList();
            ViewBag.Repuestos = db.Repuestos.Where(r => repuestosIds.Contains(r.Id) && r.CantidadEnStock > 0).ToList();

            return View();
        }
        // Método para agregar un vehículo al carrito
        [HttpPost]
        public ActionResult AgregarVehiculoAlCarrito(int carroId)
        {
            var usuarioViewModel = Session["UsuarioActual"] as UsuariosViewModel;
            if (usuarioViewModel == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = db.Usuarios.Find(usuarioViewModel.Id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            var carro = db.Carros.Find(carroId);
            if (carro == null || carro.CantidadEnStock <= 0)
            {
                TempData["Message"] = "El vehículo no está disponible en stock";
                return RedirectToAction("Index");
            }

            var productosEnCarrito = usuario.ProductosEnCarrito?.Split(',').ToList() ?? new List<string>();
            if (!productosEnCarrito.Contains($"carro:{carroId}"))
            {
                productosEnCarrito.Add($"carro:{carroId}");
                usuario.ProductosEnCarrito = string.Join(",", productosEnCarrito);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Método para agregar un repuesto al carrito
        [HttpPost]
        public ActionResult AgregarRepuestoAlCarrito(int repuestoId)
        {
            var usuarioViewModel = Session["UsuarioActual"] as UsuariosViewModel;
            if (usuarioViewModel == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = db.Usuarios.Find(usuarioViewModel.Id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            var repuesto = db.Repuestos.Find(repuestoId);
            if (repuesto == null || repuesto.CantidadEnStock <= 0)
            {
                TempData["Message"] = "El repuesto no está disponible en stock";
                return RedirectToAction("Index");
            }

            var productosEnCarrito = usuario.ProductosEnCarrito?.Split(',').ToList() ?? new List<string>();
            if (!productosEnCarrito.Contains($"repuesto:{repuestoId}"))
            {
                productosEnCarrito.Add($"repuesto:{repuestoId}");
                usuario.ProductosEnCarrito = string.Join(",", productosEnCarrito);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EliminarDelCarrito(int id, string tipo)
        {
            var usuarioViewModel = Session["UsuarioActual"] as UsuariosViewModel;
            if (usuarioViewModel == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = db.Usuarios.Find(usuarioViewModel.Id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            var productosEnCarrito = usuario.ProductosEnCarrito?.Split(',').ToList() ?? new List<string>();
            var productoARemover = productosEnCarrito.FirstOrDefault(p => p == $"{tipo}:{id}");

            if (productoARemover != null)
            {
                productosEnCarrito.Remove(productoARemover);
                usuario.ProductosEnCarrito = string.Join(",", productosEnCarrito);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Carrito/CheckOut
        public ActionResult CheckOut()
        {
            var usuarioViewModel = Session["UsuarioActual"] as UsuariosViewModel;
            if (usuarioViewModel == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = db.Usuarios.Find(usuarioViewModel.Id);
            var productosEnCarrito = usuario.ProductosEnCarrito;

            if (productosEnCarrito == null || !productosEnCarrito.Any())
            {
                return RedirectToAction("Index");
            }

            // Calcular el total
            decimal total = 0;
            foreach (var productoIdStr in productosEnCarrito)
            {
                // Convertir el productoId a entero
                if (int.TryParse(productoIdStr.ToString(), out int productoId))
                {
                    // Encontrar el producto en Repuestos
                    var repuesto = db.Repuestos.Find(productoId);
                    if (repuesto != null)
                    {
                        total += repuesto.Precio ?? 0;
                    }
                    else
                    {
                        // Si no se encuentra en Repuestos, buscar en Carros
                        var carro = db.Carros.Find(productoId);
                        if (carro != null)
                        {
                            total += carro.Precio ?? 0;
                        }
                    }
                }
            }

            ViewBag.Total = total;
            return View();
        }

        // POST: Carrito/Checkout
        [HttpPost]
        public ActionResult CheckOut(string cardNumber, string expiryDate, string cvv)
        {
            // Aquí se procesa el pago
            TempData["Message"] = "Compra realizada";

            var usuarioViewModel = Session["UsuarioActual"] as UsuariosViewModel;
            if (usuarioViewModel == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var usuario = db.Usuarios.Find(usuarioViewModel.Id);
            var productosEnCarrito = usuario.ProductosEnCarrito?.Split(',') ?? new string[] { };

            // Calcular el total y reducir el stock
            decimal total = 0;
            foreach (var productoIdStr in productosEnCarrito)
            {
                var parts = productoIdStr.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out int productoId))
                {
                    var repuesto = db.Repuestos.Find(productoId);
                    if (repuesto != null)
                    {
                        if (repuesto.CantidadEnStock > 0)
                        {
                            total += repuesto.Precio ?? 0;
                            repuesto.CantidadEnStock -= 1;

                        }
                    }
                    else
                    {
                        var carro = db.Carros.Find(productoId);
                        if (carro != null)
                        {
                            if (carro.CantidadEnStock > 0)
                            {
                                total += carro.Precio ?? 0;
                                carro.CantidadEnStock -= 1;

                            }
                        }
                    }
                }
            }

            // Limpiar el carrito después de la compra
            usuario.ProductosEnCarrito = null;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        private bool ValidateCard(string cardNumber, string expiryDate, string cvv, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validar número de tarjeta (16 dígitos)
            if (cardNumber.Length != 16 || !cardNumber.All(char.IsDigit))
            {
                errorMessage = "El número de tarjeta debe tener 16 dígitos.";
                return false;
            }

            // Validar CVV (3 dígitos)
            if (cvv.Length != 3 || !cvv.All(char.IsDigit))
            {
                errorMessage = "El CVV debe tener 3 dígitos.";
                return false;
            }

            // Validar fecha de expiración
            if (!DateTime.TryParseExact(expiryDate, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime expiryDateParsed) ||
                expiryDateParsed < DateTime.Now)
            {
                errorMessage = "La fecha de expiracion debe ser una fecha valida, no puede ser anterior a la fecha actual.";
                return false;
            }

            return true;
        }
    }
}