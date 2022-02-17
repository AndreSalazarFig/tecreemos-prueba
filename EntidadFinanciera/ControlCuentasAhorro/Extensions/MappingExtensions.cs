using ControlCuentasAhorro.Domain;
using ControlCuentasAhorro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlCuentasAhorro.Extensions
{
    public static class MappingExtensions
    {
        public static IEnumerable<ClienteVM> ToClienteVM(this IEnumerable<Cliente> clientes)
        {
            return clientes.Select(x => new ClienteVM
            {
                IdCliente = x.IdCliente,
                ApellidoMaterno = x.ApellidoMaterno,
                ApellidoPaterno = x.ApellidoPaterno,
                Nombre = x.Nombre,
                NumeroCliente = x.NumeroCliente
            });
        }

        public static ClienteVM ToClienteVM(this Cliente cliente)
        {
            return new ClienteVM
            {
                IdCliente = cliente.IdCliente,
                ApellidoMaterno = cliente.ApellidoMaterno,
                ApellidoPaterno = cliente.ApellidoPaterno,
                Nombre = cliente.Nombre,
                NumeroCliente = cliente.NumeroCliente
            };
        }

        public static Cliente ToCliente(this ClienteVM cliente)
        {
            return new Cliente
            {
                IdCliente = cliente.IdCliente,
                ApellidoMaterno = cliente.ApellidoMaterno,
                ApellidoPaterno = cliente.ApellidoPaterno,
                Nombre = cliente.Nombre,
                NumeroCliente = cliente.NumeroCliente
            };
        }

        public static IEnumerable<CuentaAhorroVM> ToCuentaAhorroVM(this IEnumerable<CuentaAhorro> cuentas)
        {
            return cuentas.Select(x => new CuentaAhorroVM
            {
                Cliente = new ClienteVM
                {
                    IdCliente = x.IdCliente
                },
                IdCuentaAhorro = x.IdCuentaAhorro,
                NumeroCuenta = x.NumeroCuenta,
                SaldoActual = x.SaldoActual
            });
        }

        public static CuentaAhorroVM ToCuentaAhorroVM(this CuentaAhorro cuenta)
        {
            return new CuentaAhorroVM
            {
                Cliente = new ClienteVM
                {
                    IdCliente = cuenta.IdCliente
                },
                IdCuentaAhorro = cuenta.IdCuentaAhorro,
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoActual = cuenta.SaldoActual
            };
        }

        public static CuentaAhorro ToCuentaAhorro(this CuentaAhorroVM cuenta)
        {
            return new CuentaAhorro
            {
                IdCliente = cuenta.Cliente.IdCliente,
                IdCuentaAhorro = cuenta.IdCuentaAhorro,
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoActual = cuenta.SaldoActual
            };
        }

        public static IEnumerable<HistorialTransaccionVM> ToHistorialTransaccionVM(this IEnumerable<Transaccion> transacciones)
        {
            return transacciones.Select(x => new HistorialTransaccionVM
            {
                Monto = x.Monto,
                SaldoAnterior = x.SaldoAnterior,
                SaldoActual = x.SaldoActual,
                FechaOperacion = x.FechaOperacion,
                IdTipoOperacion = x.IdTipoOperacion
            });
        }

        public static Transaccion ToTransaccion(this TransaccionVM transaccion)
        {
            return new Transaccion
            {
                IdCuentaAhorro = transaccion.IdCuentaAhorro,
                Monto = transaccion.Monto,
                IdTipoOperacion = transaccion.IdTipoOperacion
            };
        }
    }
}
