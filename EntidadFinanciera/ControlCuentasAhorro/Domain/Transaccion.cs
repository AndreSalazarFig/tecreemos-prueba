using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlCuentasAhorro.Domain
{
    public class Transaccion
    {
        public int IdTransaccion { get; set; }
        public int IdCuentaAhorro { get; set; }
        public int IdTipoOperacion { get; set; }
        public decimal Monto { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoActual { get; set; }
        public DateTime FechaOperacion { get; set; }
    }
}
