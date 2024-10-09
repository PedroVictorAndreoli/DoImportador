using DoImportador.Enum;
using DoImportador.Utils;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;

using System.Data;
using System.Xml.Linq;


namespace DoImportador.Connection
{
    public class DOConn
    {

        private IDbTransaction mvarDoTransaction;
        private IDbConnection mvarDoConnection;
        private Int32 mvarDoID = -1000;
        private EnumProviderType mvarProviderType;

        public void Dispose()
        {
            if (mvarDoTransaction != null) { mvarDoTransaction.Dispose(); }
            if (mvarDoConnection != null)
            {
                if (mvarDoConnection.State != ConnectionState.Closed)
                {
                    this.ConnectionClose(mvarDoConnection);
                    mvarDoConnection.Dispose();
                }
            }
        }

        public DOConn()
        {
            mvarProviderType = EnumProviderType.SQLServer;
        }
        public DOConn(EnumProviderType providerType)
        {
            mvarProviderType = providerType;
        }

        public IDbTransaction DoTransaction
        {
            get { return mvarDoTransaction; }
            set { mvarDoTransaction = value; }
        }

        public IDbConnection DoConnection
        {
            get { return mvarDoConnection; }
            set { mvarDoConnection = value; }
        }
        public EnumProviderType ProviderType
        {
            get { return mvarProviderType; }
        }

        public Int32 DoID
        {
            get { return mvarDoID; }
            set { mvarDoID = value; }
        }

        //atmusinf_Control-1000 base de dados de demonstração para o site
        public void ConnectionOpen(string dbname, EnumDataLake datalake, bool pTransaction = false)
        {

            mvarDoConnection = GetNewConnection(dbname, datalake);
            

            if (pTransaction == true)
            {
                mvarDoTransaction = mvarDoConnection.BeginTransaction();
            }

        }

        //sisFlexControl_1000 base de dados de demonstração para o site
        public IDbConnection GetNewConnection(string doID, EnumDataLake datalake)
        {
            IDbConnection cnn;
            string cnnString = "";

            cnnString = getConnectionString(doID, datalake);

            if(EnumDataLake.ORIGIN == datalake)
            {
                cnn = DOFunctions._connectionProperties.dbType switch
                {
                    EnumProviderType.SQLServer => new SqlConnection(cnnString),
                    EnumProviderType.MySql => new MySqlConnection(cnnString),
                    EnumProviderType.PostGreSQL => new NpgsqlConnection(cnnString),
                    _ => new SqlConnection(cnnString)
                };
                
            }
            else
            {
                cnn = new SqlConnection(cnnString);
            }

            cnn.Open();

            return cnn;
        }

        //sisFlexControl_1000 base de dados de demonstração para o site
        public string getConnectionString(string dbname, EnumDataLake datalake)
        {
            return GetNewConnectionString(dbname, datalake);
        }

        //sisFlexControl_1000 base de dados de demonstração para o site
        public String GetNewConnectionString(string dbname, EnumDataLake datalake, EnumProviderType providerType = EnumProviderType.Auto)
        {
            if(datalake == EnumDataLake.ORIGIN)
            {

                return DOFunctions._connectionProperties.dbType switch
                {
                    EnumProviderType.MySql => $"Server={DOFunctions._connectionProperties.hostOrigin};Port={DOFunctions._connectionProperties.portOrigin};Database={DOFunctions._connectionProperties.dbNameOrigin};Uid={DOFunctions._connectionProperties.userOrigin};Pwd={DOFunctions._connectionProperties.passwordOrigin}",
                    EnumProviderType.PostGreSQL => $"Host={DOFunctions._connectionProperties.hostOrigin};Port={DOFunctions._connectionProperties.portOrigin};Database={DOFunctions._connectionProperties.dbNameOrigin};Username={DOFunctions._connectionProperties.userOrigin};Password={DOFunctions._connectionProperties.passwordOrigin};",
                    _ => $"Connection Timeout=120;Persist Security Info=False; Data Source={DOFunctions._connectionProperties.hostOrigin};Initial Catalog={DOFunctions._connectionProperties.dbNameOrigin};User ID={DOFunctions._connectionProperties.userOrigin};Password={DOFunctions._connectionProperties.passwordOrigin};TrustServerCertificate=True;"

                };
            } else
            {
                return $"Connection Timeout=120;Persist Security Info=False; Data Source={DOFunctions._connectionProperties.hostDestination};Initial Catalog={DOFunctions._connectionProperties.dbNameDestination};User ID={DOFunctions._connectionProperties.userDestination};Password={DOFunctions._connectionProperties.passwordDestination};TrustServerCertificate=True;";

            }

        }

        public void ConnectionClose(IDbConnection pCnn, EnumProviderType providerType = EnumProviderType.Auto)
        {
            if (pCnn != null)
            {
                if (pCnn.State != System.Data.ConnectionState.Closed)
                {
                    pCnn.Close();
                }
                pCnn.Dispose();

                switch (providerType)
                {
                    case EnumProviderType.PostGreSQL:
                        NpgsqlConnection.ClearAllPools();
                        NpgsqlConnection.ClearPool((NpgsqlConnection)pCnn);
                        break;
                    case EnumProviderType.MySql:
                        MySqlConnection.ClearAllPools();
                        MySqlConnection.ClearPool((MySqlConnection)pCnn);
                        break;
                    default: // providerType.SQLServer
                        SqlConnection.ClearAllPools();
                        SqlConnection.ClearPool((SqlConnection)pCnn);
                        break;
                }
            }
        }

        public IDbCommand GetNewCommand(string cmdText, IDbConnection cnn, int timeOut = 60, EnumProviderType providerType = EnumProviderType.Auto)
        {
            IDbCommand command = null;


            switch (providerType)
            {
                case EnumProviderType.PostGreSQL:
                    //As consultas foram feitas para SQL Server, então tenho
                    //que normalizar ou parsear o SQL para o PostgreSQL
                    cmdText = DOFunctions.ParseSQLToPostgreSQL(cmdText);
                    command = new NpgsqlCommand(cmdText, (NpgsqlConnection)cnn);
                    command.CommandTimeout = timeOut;
                    break;
                case EnumProviderType.MySql:
                    command = new MySqlCommand(cmdText, (MySqlConnection)cnn);
                    command.CommandTimeout = timeOut;
                    break;
                default: //SQLServer
                    command = new SqlCommand(cmdText, (SqlConnection)cnn);
                    command.CommandTimeout = timeOut;
                    break;
            }

            return command;
        }

        public IDbDataParameter GetNewParameter(string parameterName, object value, EnumProviderType providerType = EnumProviderType.Auto)
        {
            IDbDataParameter parameter = null;


            if (providerType == EnumProviderType.Auto) { providerType = mvarProviderType; }

            switch (providerType)
            {
                case EnumProviderType.PostGreSQL:
                    parameter = new NpgsqlParameter(parameterName, value);
                    break;
                case EnumProviderType.MySql:
                    parameter = new MySqlParameter(parameterName, value);
                    break;
                default: //SQLServer
                    parameter = new SqlParameter(parameterName, value);
                    break;
            }

            return parameter;
        }
    }
}
