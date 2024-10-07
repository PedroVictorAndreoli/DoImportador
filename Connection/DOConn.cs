using DoImportador.Enum;
using DoImportador.Utils;
using Microsoft.Data.SqlClient;
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
        private enumProviderType mvarProviderType;

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
            mvarProviderType = enumProviderType.SQLServer;
        }
        public DOConn(enumProviderType providerType)
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
        public enumProviderType ProviderType
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

            cnn = new SqlConnection(cnnString);

            cnn.Open();

            return cnn;
        }

        //sisFlexControl_1000 base de dados de demonstração para o site
        public string getConnectionString(string dbname, EnumDataLake datalake)
        {
            return GetNewConnectionString(dbname, datalake);
        }

        //sisFlexControl_1000 base de dados de demonstração para o site
        public String GetNewConnectionString(string dbname, EnumDataLake datalake)
        {
            return $"Connection Timeout=120;Persist Security Info=False; Data Source={(datalake == EnumDataLake.ORIGIN ? DOFunctions._hostOrigin : DOFunctions._hostDest)}\\MSSQLSERVER2022;Initial Catalog={dbname};User ID=atmusinf;Password=Atmus@#4080";
        }

        public void ConnectionClose(IDbConnection pCnn)
        {
            if (pCnn != null)
            {
                if (pCnn.State != System.Data.ConnectionState.Closed)
                {
                    pCnn.Close();
                }
                pCnn.Dispose();

                switch (pCnn.GetType().Name.ToUpper().Trim())
                {
                    case "NPGSQLCONNECTION":
                        NpgsqlConnection.ClearAllPools();
                        NpgsqlConnection.ClearPool((NpgsqlConnection)pCnn);
                        break;
                    default: // providerType.SQLServer
                        SqlConnection.ClearAllPools();
                        SqlConnection.ClearPool((SqlConnection)pCnn);
                        break;
                }
            }
        }

        public IDbCommand GetNewCommand(string cmdText, IDbConnection cnn, int timeOut = 60)
        {
            IDbCommand command = null;

            switch (cnn.GetType().Name.ToUpper().Trim())
            {
                case "NPGSQLCONNECTION":
                    //As consultas foram feitas para SQL Server, então tenho
                    //que normalizar ou parsear o SQL para o PostgreSQL
                    cmdText = DOFunctions.ParseSQLToPostgreSQL(cmdText);
                    command = new NpgsqlCommand(cmdText, (NpgsqlConnection)cnn);
                    command.CommandTimeout = timeOut;
                    break;
                default: //SQLServer
                    command = new SqlCommand(cmdText, (SqlConnection)cnn);
                    command.CommandTimeout = timeOut;
                    break;
            }

            return command;
        }

        public IDbDataParameter GetNewParameter(string parameterName, object value, enumProviderType providerType = enumProviderType.Auto)
        {
            IDbDataParameter parameter = null;

            if (providerType == enumProviderType.Auto) { providerType = mvarProviderType; }

            switch (providerType)
            {
                case enumProviderType.PostGreSQL:
                    parameter = new NpgsqlParameter(parameterName, value);
                    break;
                default: //SQLServer
                    parameter = new SqlParameter(parameterName, value);
                    break;
            }

            return parameter;
        }

        public IDbDataParameter GetNewParameter(string parameterName, SqlDbType parameterType, enumProviderType providerType = enumProviderType.Auto)
        {
            IDbDataParameter parameter = null;

            if (providerType == enumProviderType.Auto) { providerType = mvarProviderType; }

            switch (providerType)
            {
                case enumProviderType.PostGreSQL:
                    parameter = new NpgsqlParameter(parameterName, this.ConvertSQLDbTypeToNpgsqlDbType(parameterType));
                    break;
                default: //SQLServer
                    parameter = new SqlParameter(parameterName, (SqlDbType)parameterType);
                    break;
            }

            return parameter;
        }
        public IDbDataParameter GetNewParameter(string parameterName, SqlDbType parameterType, int size, enumProviderType providerType = enumProviderType.Auto)
        {
            IDbDataParameter parameter = null;

            if (providerType == enumProviderType.Auto) { providerType = mvarProviderType; }

            switch (providerType)
            {
                case enumProviderType.PostGreSQL:
                    parameter = new NpgsqlParameter(parameterName, this.ConvertSQLDbTypeToNpgsqlDbType(parameterType));
                    break;
                default: //SQLServer
                    parameter = new SqlParameter(parameterName, (SqlDbType)parameterType, size);
                    break;
            }

            return parameter;
        }

        public DbType ConvertSQLDbTypeToNpgsqlDbType(SqlDbType type)
        {
            DbType ret = DbType.Object;

            switch (type)
            {
                case SqlDbType.BigInt:
                    ret = DbType.Int64;
                    break;
                case SqlDbType.Binary:
                    ret = DbType.Binary;
                    break;
                case SqlDbType.Bit:
                    ret = DbType.Boolean;
                    break;
                case SqlDbType.Char:
                    ret = DbType.String;
                    break;
                case SqlDbType.Date:
                    ret = DbType.Date;
                    break;
                case SqlDbType.DateTime2:
                    ret = DbType.DateTime2;
                    break;
                case SqlDbType.DateTimeOffset:
                    ret = DbType.DateTimeOffset;
                    break;
                case SqlDbType.Decimal:
                    ret = DbType.Decimal;
                    break;
                case SqlDbType.Float:
                    ret = DbType.Decimal;
                    break;
                case SqlDbType.Image:
                    ret = DbType.Binary;
                    break;
                case SqlDbType.Int:
                    ret = DbType.Int32;
                    break;
                case SqlDbType.Money:
                    ret = DbType.Currency;
                    break;
                case SqlDbType.NChar:
                    ret = DbType.String;
                    break;
                case SqlDbType.Real:
                    ret = DbType.Decimal;
                    break;
                case SqlDbType.SmallDateTime:
                    ret = DbType.DateTime;
                    break;
                case SqlDbType.SmallInt:
                    ret = DbType.Int16;
                    break;
                case SqlDbType.SmallMoney:
                    ret = DbType.Currency;
                    break;
                case SqlDbType.Text:
                    ret = DbType.String;
                    break;
                case SqlDbType.UniqueIdentifier:
                    ret = DbType.Guid;
                    break;
                case SqlDbType.Time:
                    ret = DbType.Time;
                    break;
                case SqlDbType.Timestamp:
                    ret = DbType.Time;
                    break;
                case SqlDbType.TinyInt:
                    ret = DbType.Int16;
                    break;
                case SqlDbType.Xml:
                    ret = DbType.Xml;
                    break;
                default:
                    ret = DbType.Object;
                    break;
            }

            return ret;
        }
    }
}
