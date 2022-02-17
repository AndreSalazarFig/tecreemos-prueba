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
    public class TransaccionService : ITransaccionService
    {
        readonly IDbService _dbService;

        public TransaccionService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<Transaccion> ObtenerTransacciones(int idcuenta)
        {
            string consulta = $"SELECT * FROM Transaccion WHERE IdCuentaAhorro = {idcuenta}";
            var tabla = _dbService.LeerTabla(consulta);

            return tabla.AsEnumerable().Select(row => new Transaccion
            {
                IdTransaccion = Convert.ToInt32(row["IdTransaccion"]),
                IdCuentaAhorro = Convert.ToInt32(row["IdCuentaAhorro"]),
                IdTipoOperacion = Convert.ToInt32(row["IdTipoOperacion"]),
                Monto = Convert.ToDecimal(row["Monto"]),
                SaldoActual = Convert.ToDecimal(row["SaldoActual"]),
                SaldoAnterior = Convert.ToDecimal(row["SaldoAnterior"]),
                FechaOperacion = Convert.ToDateTime(row["FechaOperacion"].ToString())
            });
        }

        public int CrearTransaccion(Transaccion model)
        {
            string sentencia = @"
Declare @SaldoAnterior decimal(8,2) = (Select top 1 SaldoActual from CuentaAhorro where IdCuentaAhorro = @IdCuentaAhorro);

Insert into Transaccion(
        IdCuentaAhorro,
        IdTipoOperacion,
        Monto,
        SaldoActual,
        SaldoAnterior,
        FechaOperacion
)
values (
        @IdCuentaAhorro,
        @IdTipoOperacion,
        @Monto,
        @SaldoAnterior + @Monto,
        @SaldoAnterior,
        @FechaOperacion
);

Update CuentaAhorro set
        SaldoActual = @SaldoAnterior + @Monto
Where IdCuentaAhorro = @IdCuentaAhorro
";

            if ((int)Domain.Enums.EnumTipoOperacionCuentaAhorro.Retiro == model.IdTipoOperacion)
                model.Monto *= -1;

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@IdCuentaAhorro", model.IdCuentaAhorro),
                new SqlParameter("@IdTipoOperacion", model.IdTipoOperacion),
                new SqlParameter("@Monto", model.Monto),
                new SqlParameter("@FechaOperacion", DateTime.Now){ SqlDbType = SqlDbType.DateTime }
            };

            return _dbService.Ejecutar(sentencia, parameters);
        }
    }
}
