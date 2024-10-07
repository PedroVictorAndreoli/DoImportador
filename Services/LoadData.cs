using doAPI.Utils;
using DoImportador.Connection;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Services
{
    public class LoadData
    {
        public static List<IDictionary> LoadDataDb(string dbNameOrigin, string sql)
        {
            var doConn = new DOConn();
            try
            {
                doConn.ConnectionOpen(dbNameOrigin, Enum.EnumDataLake.ORIGIN);

                return CrudUtils.GetAll<IDictionary>(doConn.DoConnection, sql, doConn);
                
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                doConn.ConnectionClose(doConn.DoConnection);
                doConn.Dispose();
            }
            return null;
        }
    }
}
