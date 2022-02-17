using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlCuentasAhorro.Domain
{
    public class CuentaAhorro
    {
        public int IdCuentaAhorro { get; set; }
        public int IdCliente { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal SaldoActual { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
