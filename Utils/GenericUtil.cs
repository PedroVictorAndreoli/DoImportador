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

namespace DoImportador.Utils
{
    public class GenericUtil
    {
        public static object NullForEmpty(object value)
        {
            return value == null ? "" : value;
        }

        public static object NullForZero(object value)
        {
            return value == null ? 0 : value;
        }

        public static int LoadCity(DOConn iConn, string cityName)
        {
            var query = $"SELECT top(1) ID FROM cidades where Nome like '%{cityName}%'";

            var output = CrudUtils.GetOne<IDictionary>(iConn.DoConnection, query, iConn);
            if(output.Count == 0)
            {
                return 0;
            }
            return Int32.Parse(output["ID"].ToString());
            
        }

        public static int LoadByID(DOConn iConn, string description, string table, string column = "descricao")
        {
            var query = $"SELECT top(1) ID FROM {table} where {column} like '%{description}%'";

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
    }
}
