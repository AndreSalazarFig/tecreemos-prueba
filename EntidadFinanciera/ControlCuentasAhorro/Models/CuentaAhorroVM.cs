using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlCuentasAhorro.Models
{
    public class CuentaAhorroVM
    {
        public int IdCuentaAhorro { get; set; }

        [Required]
        [DisplayName("Número de cuenta")]
        [MaxLength(5)]
        [MinLength(5)]
        [RegularExpression("^[0-9]{5}$")]
        public string NumeroCuenta { get; set; }

        [Required]
        [DisplayName("Saldo actual")]
        [DataType(DataType.Currency)]
        public decimal SaldoActual { get; set; }

        public ClienteVM Cliente { get; set; }
    }
}
