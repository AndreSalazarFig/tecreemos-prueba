using ControlCuentasAhorro.Domain;
using System.Collections.Generic;

namespace ControlCuentasAhorro.Services
{
    public interface ICuentaAhorroService
    {
        int CrearCuentaAhorro(CuentaAhorro model);
        int EditarCuentaAhorro(CuentaAhorro model);
        CuentaAhorro ObtenerCuentaAhorro(int idCuentaAhorro);
        IEnumerable<CuentaAhorro> ObtenerCuentaAhorros(int idcliente);
    }
}