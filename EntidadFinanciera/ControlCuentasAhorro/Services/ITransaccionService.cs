using ControlCuentasAhorro.Domain;
using System.Collections.Generic;

namespace ControlCuentasAhorro.Services
{
    public interface ITransaccionService
    {
        int CrearTransaccion(Transaccion model);
        IEnumerable<Transaccion> ObtenerTransacciones(int idcuenta);
    }
}