using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EmployeeClient
{
    public static class JsonUtil
    {
        public static JObject Deserialize(string json)
        {
            return (JObject) JsonConvert.DeserializeObject(json);
        }
    }
}