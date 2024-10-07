using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace doAPI.Utils
{
    public class JsonUtil
    {

        public static string DoJsonSerializer(object obj)
        {
            if (obj != null) { 
                return JsonConvert.SerializeObject(obj);
            }
            return default;
        }

        public static T DoJsonDeserialize<T>(string json)
        {
            if (json != null) { 
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }

    }
}
