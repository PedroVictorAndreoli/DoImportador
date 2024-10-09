using System.Data;
using System.Collections;
using Newtonsoft.Json;
using DoImportador.Connection;
using DoImportador.Utils;
using Microsoft.Data.SqlClient;
using DoImportador.Enum;

namespace doAPI.Utils
{
    public class CrudUtils
    {
        /**
         * Método criado para facilitar a execução de queryes(INSERT, DELETE e UPDATE) no banco
         * 
         * para utilizá-lo é necessário passar um hashtable contendo parametros e valores ex:
         * 
         * var table = new Hashtable();
         * table.Add("<<ColunaNoBanco>>", <<Valor>>);
         * 
         * e a query normal ex:
         * UPDATE tableName SET key=@key WHERE ID=@ID 
         * 
        **/
        public static bool ExecuteQuery(DOConn DOConn, Hashtable parameters, string query, EnumProviderType provider = EnumProviderType.Auto)
        {
            IDbCommand command = null;
            
            IDbConnection cnn = DOConn.DoConnection;
            command = DOConn.GetNewCommand(query, cnn,providerType: provider);
            command.CommandTimeout = 120;
            if(parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.Add(DOConn.GetNewParameter($"@{entry.Key}", entry.Value, provider));
                }
            }
            
            command.Transaction = DOConn.DoTransaction;
            command.ExecuteNonQuery();
            command.Dispose();
            return true;
        }

        /**
         * Método criado para facilitar a execução de queryes(INSERT) no banco
         * 
         * Deve ser enviado junto o comando Select Scope_Identity()
         *
         * para utilizá-lo é necessário passar um hashtable contendo parametros e valores ex:
         * 
         * var table = new Hashtable();
         * table.Add("<<ColunaNoBanco>>", <<Valor>>);
         * 
         * e a query normal ex:
         * UPDATE tableName SET key=@key WHERE ID=@ID 
         * 
        **/
        public static object ExecuteScalar(DOConn DOConn, Hashtable parameters, string query, EnumProviderType provider = EnumProviderType.Auto)
        {
            IDbCommand command = null;

            IDbConnection cnn = DOConn.DoConnection;
            command = DOConn.GetNewCommand(query, cnn, providerType: provider);
            command.CommandTimeout = 120;
            if (parameters != null)
            {
                foreach (DictionaryEntry entry in parameters)
                {
                    command.Parameters.Add(DOConn.GetNewParameter($"@{entry.Key}", entry.Value,provider));
                }
            }

            command.Transaction = DOConn.DoTransaction;
            var ret = command.ExecuteScalar();
            command.Dispose();
            return ret;
        }

        /**
         * Método criado para retornar um registro genérico do banco
         *
         * Basta passar um select, e a conexão, o restante se resolve
         */
        public static Object GetOne(IDbConnection con, String sql, DOConn doConn)
        {
            Object obj = ConstructorCommand(con, sql, 1, doConn);
            return obj;
        }

        /**
         * Método criado para retornar um registro atralado a uma classe do banco
         *
         * Basta passar um select, e a conexão, o restante se resolve
         */
        public static T GetOne<T>(IDbConnection con, String sql, DOConn doConn)
        {
            Object ret = new();

            ret = ConstructorCommand(con, sql, 1, doConn);
            var json = JsonConvert.SerializeObject(ret);
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
        /**
         * Método criado para retornar vários registro atralado a uma classe do banco
         *
         * Basta passar um select, e a conexão, o restante se resolve
         */
        public static Object GetAll(IDbConnection con, String sql, DOConn doConn)
        {
            Object obj = ConstructorCommand(con, sql, 2, doConn);
            return obj;
        }
        /**
         * Método criado para retornar vários registro atralado a uma classe do banco
         *
         * Basta passar um select, e a conexão, o restante se resolve
         */
        public static List<T> GetAll<T>(IDbConnection con, String sql, DOConn doConn)
        {

            Object ret = new();
            ret = ConstructorCommand(con, sql, 2, doConn);
            var json = JsonConvert.SerializeObject(ret);

            List<T> obj = JsonConvert.DeserializeObject<List<T>>(json);

            return obj;
        }

        public static IDbConnection GetConnection(string doID, EnumDataLake datalake)
        {
            DOConn iConn = new();
            IDbConnection cnn = null;

            if (DOFunctions.DOConnTrans != null)
            {
                iConn = DOFunctions.DOConnTrans;
                if (iConn.DoConnection != null && iConn.DoConnection.State == ConnectionState.Open)
                {
                    cnn = iConn.DoConnection;
                }
                else
                {
                    cnn = iConn.GetNewConnection(doID,datalake);
                }
            }
            else
            {
                iConn = new DOConn();
                cnn = iConn.GetNewConnection(doID, datalake);
            }

            return cnn;
        }

        private static Object ConstructorCommand(IDbConnection con, string sql, int type, DOConn doConn)
        {
            SqlCommand command = new SqlCommand(sql, (SqlConnection)con);
            if (doConn != null)
            {
                if (doConn.DoTransaction != null)
                {
                    command.Transaction = (SqlTransaction)doConn.DoTransaction;
                }
            }
            SqlDataReader dr = command.ExecuteReader();

            Hashtable table = new();
            List<Hashtable> tbl = new List<Hashtable>();

            if (type == 1)
            {
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        table.Add(dr.GetName(i), (Object)dr.GetValue(dr.GetOrdinal(dr.GetName(i))));
                    }
                }
                command.Dispose();
                dr.Close();
                return table;
            }
            else if (type == 2)
            {
                while (dr.Read())
                {
                    table = new();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        table.Add(dr.GetName(i), (Object)dr.GetValue(dr.GetOrdinal(dr.GetName(i))));
                    }
                    tbl.Add(table);

                }
                command.Dispose();
                dr.Close();
                return tbl;
            }
            return false;
        }


    }
}
