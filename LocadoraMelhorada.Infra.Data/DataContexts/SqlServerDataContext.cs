using LocadoraMelhorada.Infra.Settings;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LocadoraMelhorada.Infra.Data.DataContexts
{
    public class SqlServerDataContext : IDisposable
    {
        public SqlConnection SqlServerConexao { get; set; }

        public SqlServerDataContext(SqlServerSettings settings)
        {
            SqlServerConexao = new SqlConnection(settings.ConnectionString);
            SqlServerConexao.Open();
        }

        public void Dispose()
        {
            if (SqlServerConexao.State != ConnectionState.Closed)
                SqlServerConexao.Close();
        }
    }
}
