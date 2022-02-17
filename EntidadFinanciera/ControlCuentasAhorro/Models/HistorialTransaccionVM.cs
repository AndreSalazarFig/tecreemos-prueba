using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlCuentasAhorro.Models
{
    public class HistorialTransaccionVM
    {
        public int IdTipoOperacion { get; set; }
        public decimal Monto { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoActual { get; set; }
        public DateTime FechaOperacion { get; set; }
    }
}
