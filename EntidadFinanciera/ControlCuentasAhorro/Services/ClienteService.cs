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
    public class ClienteService : IClienteService
    {
        readonly IDbService _dbService;

        public ClienteService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<Cliente> ObtenerClientes()
        {
            string consulta = "SELECT * FROM Clientes";
            var tabla = _dbService.LeerTabla(consulta);

            return tabla.AsEnumerable().Select(row => new Cliente
            {
                IdCliente = Convert.ToInt32(row["IdCliente"]),
                ApellidoMaterno = row["ApellidoMaterno"].ToString(),
                ApellidoPaterno = row["ApellidoPaterno"].ToString(),
                Nombre = row["Nombre"].ToString(),
                NumeroCliente = row["NumeroCliente"].ToString(),
                FechaAlta = Convert.ToDateTime(row["FechaAlta"].ToString())
            });
        }

        public Cliente ObtenerCliente(int idcliente)
        {
            string consulta = $"SELECT * FROM Clientes WHERE IdCliente = {idcliente}";

            var tabla = _dbService.LeerTabla(consulta);

            return tabla.AsEnumerable().Select(row => new Cliente
            {
                IdCliente = Convert.ToInt32(row["IdCliente"]),
                ApellidoMaterno = row["ApellidoMaterno"].ToString(),
                ApellidoPaterno = row["ApellidoPaterno"].ToString(),
                Nombre = row["Nombre"].ToString(),
                NumeroCliente = row["NumeroCliente"].ToString(),
                FechaAlta = Convert.ToDateTime(row["FechaAlta"].ToString())
            }).FirstOrDefault();
        }

        public int CrearCliente(Cliente model)
        {
            string sentencia = @"
Insert into Clientes(
        Nombre,
        ApellidoPaterno,
        ApellidoMaterno,
        NumeroCliente,
        FechaAlta
)
values (
        @Nombre,
        @ApellidoPaterno,
        @ApellidoMaterno,
        @NumeroCliente,
        @FechaAlta
)";
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@Nombre", model.Nombre),
                new SqlParameter("@ApellidoPaterno", model.ApellidoPaterno),
                new SqlParameter("@ApellidoMaterno", model.ApellidoMaterno),
                new SqlParameter("@NumeroCliente", model.NumeroCliente),
                new SqlParameter("@FechaAlta", DateTime.Now){ SqlDbType = SqlDbType.DateTime }
            };

            return _dbService.Ejecutar(sentencia, parameters);
        }

        public int EditarCliente(Cliente model)
        {
            string sentencia = @"
Update Clientes set
        Nombre = @Nombre,
        ApellidoPaterno = @ApellidoPaterno,
        ApellidoMaterno = @ApellidoMaterno,
        NumeroCliente = @NumeroCliente
Where IdCliente = @IdCliente";
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@IdCliente", model.IdCliente),
                new SqlParameter("@Nombre", model.Nombre),
                new SqlParameter("@ApellidoPaterno", model.ApellidoPaterno),
                new SqlParameter("@ApellidoMaterno", model.ApellidoMaterno),
                new SqlParameter("@NumeroCliente", model.NumeroCliente),
            };

            return _dbService.Ejecutar(sentencia, parameters);
        }
    }
}
