using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomerClient;

public static class JsonUtil
{
    public static JObject Deserialize(string json)
    {
        return (JObject) JsonConvert.DeserializeObject(json);
    }
}