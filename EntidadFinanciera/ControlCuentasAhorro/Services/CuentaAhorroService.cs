using ControlCuentasAhorro.Context;
using ControlCuentasAhorro.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ControlCuentasAhorro.Services
{
    public class CuentaAhorroService : ICuentaAhorroService
    {
        readonly IDbService _dbService;

        public CuentaAhorroService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<CuentaAhorro> ObtenerCuentaAhorros(int idcliente)
        {
            string consulta = $"SELECT * FROM CuentaAhorro WHERE IdCliente = {idcliente}";
            var tabla = _dbService.LeerTabla(consulta);

            return tabla.AsEnumerable().Select(row => new CuentaAhorro
            {
                IdCuentaAhorro = Convert.ToInt32(row["IdCuentaAhorro"]),
                IdCliente = Convert.ToInt32(row["IdCliente"]),
                SaldoActual = Convert.ToDecimal(row["SaldoActual"]),
                NumeroCuenta = row["NumeroCuenta"].ToString(),
                FechaAlta = Convert.ToDateTime(row["FechaAlta"].ToString())
            });
        }

        public CuentaAhorro ObtenerCuentaAhorro(int idCuentaAhorro)
        {
            string consulta = $"SELECT * FROM CuentaAhorro WHERE IdCuentaAhorro = {idCuentaAhorro}";

            var tabla = _dbService.LeerTabla(consulta);

            return tabla.AsEnumerable().Select(row => new CuentaAhorro
            {
                IdCuentaAhorro = Convert.ToInt32(row["IdCuentaAhorro"]),
                IdCliente = Convert.ToInt32(row["IdCliente"]),
                SaldoActual = Convert.ToDecimal(row["SaldoActual"]),
                NumeroCuenta = row["NumeroCuenta"].ToString(),
                FechaAlta = Convert.ToDateTime(row["FechaAlta"].ToString())
            }).FirstOrDefault();
        }

        public int CrearCuentaAhorro(CuentaAhorro model)
        {
            string sentencia = @"
Insert into CuentaAhorro(
        IdCliente,
        SaldoActual,
        NumeroCuenta,
        FechaAlta
)
values (
        @IdCliente,
        @SaldoActual,
        @NumeroCuenta,
        @FechaAlta
)";
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@IdCliente", model.IdCliente),
                new SqlParameter("@SaldoActual", model.SaldoActual),
                new SqlParameter("@NumeroCuenta", model.NumeroCuenta),
                new SqlParameter("@FechaAlta", DateTime.Now){ SqlDbType = SqlDbType.DateTime }
            };

            return _dbService.Ejecutar(sentencia, parameters);
        }

        public int EditarCuentaAhorro(CuentaAhorro model)
        {
            string sentencia = @"
Update CuentaAhorro set
        SaldoActual = @SaldoActual,
        NumeroCuenta = @NumeroCuenta
Where IdCuentaAhorro = @IdCuentaAhorro";
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@IdCuentaAhorro", model.IdCuentaAhorro),
                new SqlParameter("@SaldoActual", model.SaldoActual),
                new SqlParameter("@NumeroCuenta", model.NumeroCuenta),
            };

            return _dbService.Ejecutar(sentencia, parameters);
        }
    }
}
