using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProyectoFinal_ActivosFijos.Models.TableViewModel
{
    public class CarrosTableViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La marca es requerida")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es requerido")]
        public string Modelo { get; set; }
                
        [Display(Name = "Año")]
        [Required(ErrorMessage = "El año es requerido")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La transmision es requerida")]
        public string Transmision { get; set; }

        [Required(ErrorMessage = "El combustible es requerido")]
        public string Combustible { get; set; }

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