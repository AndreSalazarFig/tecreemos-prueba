using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ControlCuentasAhorro.Configuration.Options;

namespace ControlCuentasAhorro.Context
{
    public class DbService : IDbService
    {
        readonly DbSettingsOption _dbSettingsOption;
        int TiempoEspera = 300;
        CommandType TipoConsulta = CommandType.Text;

        public DbService(DbSettingsOption dbSettingsOption)
        {
            _dbSettingsOption = dbSettingsOption;
        }

        SqlConnection CrearConexion()
        {
            var conexion = new SqlConnection(_dbSettingsOption.ConnectionString);
            conexion.Open();
            return conexion;
        }

        public SqlDataReader Leer(string consulta, IEnumerable<SqlParameter> sqlParameters = null)
        {
            using (var conexion = CrearConexion())
            {
                SqlCommand command = conexion.CreateCommand();
                command.CommandText = consulta;
                command.CommandTimeout = TiempoEspera;
                command.CommandType = TipoConsulta;

                if (sqlParameters != null)
                    command.Parameters.AddRange(sqlParameters.ToArray());

                return command.ExecuteReader();
            }
        }

        public DataTable LeerTabla(string consulta, IEnumerable<SqlParameter> sqlParameters = null)
        {
            using (var conexion = CrearConexion())
            {
                SqlCommand command = conexion.CreateCommand();
                command.CommandText = consulta;
                command.CommandTimeout = TiempoEspera;
                command.CommandType = TipoConsulta;

                if (sqlParameters != null)
                    command.Parameters.AddRange(sqlParameters.ToArray());

                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);

                return dataTable;
            }
        }

        public int Ejecutar(string consulta, IEnumerable<SqlParameter> sqlParameters = null)
        {
            using (var conexion = CrearConexion())
            {
                SqlCommand command = conexion.CreateCommand();
                command.CommandText = consulta;
                command.CommandTimeout = TiempoEspera;
                command.CommandType = TipoConsulta;

                if (sqlParameters != null)
                    command.Parameters.AddRange(sqlParameters.ToArray());

                return command.ExecuteNonQuery();
            }
        }
    }
}
