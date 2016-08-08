using Newtonsoft.Json;

namespace Jint.Serialization
{
    public static class JsonFormatter
    {
        public static string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

        }

        public static T Deserialize<T>(string value)
        {
            var instance = JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return instance;
        }
    }
}
