using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlCuentasAhorro.Models
{
    public class ClienteVM
    {
        public int IdCliente { get; set; }

        [Required]
        [DisplayName("Nombre(s)")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [DisplayName("Apellido paterno")]
        [MaxLength(50)]
        public string ApellidoPaterno { get; set; }

        [Required]
        [DisplayName("Apellido materno")]
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [DisplayName("Número de cliente")]
        [MaxLength(5)]
        [MinLength(5)]
        [RegularExpression("^[0-9]{5}$")]
        public string NumeroCliente { get; set; }
    }
}
