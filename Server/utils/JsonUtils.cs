using System.Text.Json;

namespace Server.Utils;

public static class JsonUtils
{
    public static Dictionary<string, string>? Deserialize(string json)
    {
        Dictionary<string, string>? data;

        try
        {
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        catch (Exception e)
        {
            data = null;
        }

        return data;
    }

    public static string Serialize(Dictionary<string, string> data)
    {
        string json = "{";

        foreach (var pair in data)
        {
            json += $"\"{pair.Key}\":\"{pair.Value}\",";
        }

        json = json.Substring(0, json.Length - 1);
        
        json += "}";

        return json;
    }

    public static string SerializeObject(object obj)
    {
        string json = JsonSerializer.Serialize(obj, obj.GetType(), new JsonSerializerOptions());

        return json;
    }
}