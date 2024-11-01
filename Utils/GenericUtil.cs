using doAPI.Utils;
using DoImportador.Connection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DoImportador.Utils
{
    public class GenericUtil
    {
        public static object NullForEmpty(object value)
        {
            return value == null ? "" : value;
        }

        public static object VerifyValidDateTime(object value)
        {
            DateTime data;
            bool isDataValida = DateTime.TryParse(value.ToString(), out data);
            if(data.Year < 1900 || data.Year > 3000)
            {
                isDataValida = false;
            }

            return isDataValida ? value : DBNull.Value;
        }

        public static object NullForZero(object value)
        {
            return value == null ? 0 : value;
        }

        public static int LoadCity(DOConn iConn, string cityName)
        {
            var query = $"SELECT top(1) ID FROM cidades where UPPER(Nome) COLLATE Latin1_General_CI_AI like UPPER('%{cityName}%')";

            var output = CrudUtils.GetOne<IDictionary>(iConn.DoConnection, query, iConn);
            if(output.Count == 0)
            {
                return 0;
            }
            return Int32.Parse(output["ID"].ToString());
            
        }

        public static int LoadByID(DOConn iConn, string description, string table, string column = "descricao")
        {
            var query = $"SELECT top(1) ID FROM {table} where UPPER({column}) COLLATE Latin1_General_CI_AI like UPPER('%{description}%')";

            var output = CrudUtils.GetOne<IDictionary>(iConn.DoConnection, query, iConn);
            if (output.Count == 0)
            {
                return 0;
            }
            return Int32.Parse(output["ID"].ToString());

        }

        public static int LoadID(DOConn iConn, object description, string table, string column = "ID")
        {
            var query = $"SELECT top(1) ID FROM {table} where {column} = {description}";

            var output = CrudUtils.GetOne<IDictionary>(iConn.DoConnection, query, iConn);
            if (output.Count == 0)
            {
                return 0;
            }
            return Int32.Parse(output["ID"].ToString());

        }

        public static int ReturnSexo(object sexo)
        {
            if (sexo == null) return 0;

            return sexo.ToString() == "Masculino" ? 1 : 2;
        }

        public static int ReturnTypeItem(object item)
        {
            if (item == null) return 0;

            return item.ToString() == "Produto" ? 0 : 1;
        }

        public static string ReturnNumber(object input)
        {
            return Regex.Replace(GenericUtil.NullForEmpty(input).ToString(), @"\D", "");
        }

        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
        public static string TruncateString(string value, int maxLength, byte removeAccents = 0)
        {
            if (string.IsNullOrEmpty(value)) return value;

            if (removeAccents == 0)
            {
                return value.Length <= maxLength ? value : value.Substring(0, maxLength);
            }
            else
            {
                return value.Length <= maxLength ? RemoveAccents(value) : RemoveAccents(value.Substring(0, maxLength));
            }

        }

        public static object OnConvertDateToString(object value)
        {
            if (value == null) return null;

            DateTime data = DateTime.Parse(value.ToString());

            return data;
        }

        public static string LoadFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return "";
            }
        }
    }
}
