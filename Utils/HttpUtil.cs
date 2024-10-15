using doAPI.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Utils
{
    public class HttpUtil
    {

        public static string _url = "https://localhost:5001/api/";
        public static T DoPost<T>(string url, string body, Hashtable headers = null)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Add("Accept", "application/json");
            if (headers != null)
            {
                foreach (DictionaryEntry entry in headers)
                {
                    request.Headers.Add(entry.Key.ToString(), entry.Value.ToString());
                }
            }

            var content = new StringContent(body, null, "application/json");
            client.Timeout = TimeSpan.FromSeconds(100);
            request.Content = content;
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync();
            return JsonUtil.DoJsonDeserialize<T>(result.Result);
        }

        public static T DoGet<T>(string url, string data, Hashtable headers)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", "application/json");

            if (headers != null)
            {
                foreach (DictionaryEntry entry in headers)
                {
                    request.Headers.Add(entry.Key.ToString(), entry.Value.ToString());
                }
            }
            client.Timeout = TimeSpan.FromSeconds(100);
            try
            {
                var response = client.Send(request);
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync();
                if (typeof(T) == typeof(string))
                {
                    return (T)(object)result.Result.ToString();
                }
                else
                {
                    return JsonUtil.DoJsonDeserialize<T>(result.Result);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return JsonUtil.DoJsonDeserialize<T>("");
        }
    }
}
