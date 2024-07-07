using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ActivosFijos.Models.ViewModel
{
    public class UsuariosViewModel
    {
        public int Id { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "La cédula debe contener exactamente 9 dígitos")]
        public int Cedula { get; set; }

        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "El nombre debe contener solo letras")]
        public string Nombre { get; set; }

        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "El primer apellido debe contener solo letras")]
        public string PrimerApellido { get; set; }

        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "El segundo apellido debe contener solo letras")]
        public string SegundoApellido { get; set; }

        [RegularExpression(@"^\d{1,2}$", ErrorMessage = "La edad debe ser un número con máximo 2 dígitos")]
        public int Edad { get; set; }

        [RegularExpression(@"^\d{8}$", ErrorMessage = "El teléfono debe contener exactamente 8 dígitos")]
        public int Telefono { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Por favor, introduzca una dirección de correo válida")]
        public string Correo { get; set; }

        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public int TipoDeUsuario { get; set; }
        public string Contrasena { get; set; }
        public string ProductosEnCarrito { get; set; }

    }
}