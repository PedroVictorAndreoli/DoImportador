using doAPI.Utils;
using DoImportador.Connection;
using DoImportador.Utils;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DoImportador.Services
{
    public class LoadData
    {
        
        public static List<IDictionary> LoadDataDb(string dbNameOrigin, string sql)
        {
            Form1 form1 = new Form1();
            var doConn = new DOConn();
            try
            {
                doConn.ConnectionOpen(dbNameOrigin, Enum.EnumDataLake.ORIGIN);
                var query = Regex.Replace(sql, @"[\u000B\r\n]+", " ").Replace("\v", ""); 

                return CrudUtils.GetAll<IDictionary>(doConn.DoConnection, query, doConn);
                
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
                
            }
            finally
            {
                doConn.ConnectionClose(doConn.DoConnection, DOFunctions._connectionProperties.dbType);
                doConn.Dispose();
            }
            return null;
        }

        public static List<T> LoadDataByType<T>(string dbNameOrigin, string sql)
        {
            var doConn = new DOConn();
            try
            {
                doConn.ConnectionOpen(dbNameOrigin, Enum.EnumDataLake.ORIGIN);

                return CrudUtils.GetAll<T>(doConn.DoConnection, sql, doConn);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                doConn.ConnectionClose(doConn.DoConnection, DOFunctions._connectionProperties.dbType);
                doConn.Dispose();
            }
            return new List<T>();
        }


    }
}
