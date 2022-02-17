using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ControlCuentasAhorro.Context
{
    public interface IDbService
    {
        int Ejecutar(string consulta, IEnumerable<SqlParameter> sqlParameters = null);
        SqlDataReader Leer(string consulta, IEnumerable<SqlParameter> sqlParameters = null);
        DataTable LeerTabla(string consulta, IEnumerable<SqlParameter> sqlParameters = null);
    }
}