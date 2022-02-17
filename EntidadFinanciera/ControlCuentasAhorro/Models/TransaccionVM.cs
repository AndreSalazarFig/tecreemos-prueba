using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlCuentasAhorro.Models
{
    public class TransaccionVM
    {
        public string NombreCliente { get; set; }
        public string NumeroCuenta { get; set; }
        public int IdCuentaAhorro { get; set; }
        public int IdCliente { get; set; }
        [Required]
        [DisplayName("Tipo de operación")]
        [Range(1,2, ErrorMessage = "Required")]
        public int IdTipoOperacion { get; set; }
        [Required]
        [DisplayName("Monto")]
        public decimal Monto { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
