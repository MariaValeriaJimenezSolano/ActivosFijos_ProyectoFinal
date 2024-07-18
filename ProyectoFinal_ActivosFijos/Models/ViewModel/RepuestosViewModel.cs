using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ActivosFijos.Models.ViewModel
{
    public class RepuestosViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "La marca solo puede contener letras")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es requerido")]
        public string Modelo { get; set; }


        [Required(ErrorMessage = "El estado es requerido")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El año es requerido")]
        [Range(1920, int.MaxValue, ErrorMessage = "Ingrese un año válido")]
        public Nullable<int> Anio { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe ser un número válido")]
        public Nullable<decimal> Precio { get; set; }

        [Required(ErrorMessage = "La descripcion es requerida")]
        public string Descripcion { get; set; }

        public byte[] Imagen1 { get; set; }

        public byte[] Imagen2 { get; set; }

        [Required(ErrorMessage = "La cantidad en stock es requerida")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La cantidad en stock debe ser un número entero")]
        public Nullable<int> CantidadEnStock { get; set; }
    }
}