using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ActivosFijos.Models.TableViewModel
{
    public class RepuestosTableViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        public string Marca { get; set; }


        [Required(ErrorMessage = "EL estado es requerido")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El modelo es requerido")]
        public string Modelo { get; set; }

        [Display(Name = "Año")]
        [Required(ErrorMessage = "El año es requerido")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La descripcion es requerida")]
        public string Descripcion { get; set; }
        public byte[] Imagen1 { get; set; }
        public byte[] Imagen2 { get; set; }

        [Display(Name = "Cantidad en stock")]

        [Required(ErrorMessage = "La cantidad en stock es requerida")]
        public int CantidadEnStock { get; set; }
        public bool EliminarImagen1 { get; set; }
        public bool EliminarImagen2 { get; set; }

    }
}