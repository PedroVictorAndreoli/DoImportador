using Npgsql.Replication.PgOutput.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Utils
{
    public class SecurityUtil
    {

        public static string OnLoginToken(object doID)
        {
            var body = "{ \"doID\": \""+ doID + "\", \"pLogin\":\"dataon\",    \"pPass\": \"796121\"}";

            var token = HttpUtil.DoPost<string>($"{HttpUtil._url}security/GetTokenJwt", body);

            return token;
        }
    }
}
