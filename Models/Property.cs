using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetKubernetes.Models {
    public class Property {
      [Key]
      [Required]
      public int Id { get; set; }
      public string? Nombre { get; set; }
      public string? Direccion { get; set; }

      [Required]
      [Column(TypeName = "decimal(18,4)")]
      public decimal Precio { get; set; }
      public string? Picture { get; set; }
      public DateTime? FechaCreacion { get; set; }
    }
}