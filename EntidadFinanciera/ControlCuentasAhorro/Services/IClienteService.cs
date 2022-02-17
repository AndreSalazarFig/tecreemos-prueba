using ControlCuentasAhorro.Domain;
using System.Collections.Generic;

namespace ControlCuentasAhorro.Services
{
    public interface IClienteService
    {
        int CrearCliente(Cliente model);
        int EditarCliente(Cliente model);
        Cliente ObtenerCliente(int idcliente);
        IEnumerable<Cliente> ObtenerClientes();
    }
}