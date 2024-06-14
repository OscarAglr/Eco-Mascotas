using Eco_Mascotas.Models;
using Newtonsoft.Json;

namespace Eco_Mascotas.Utility
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

        public static Dictionary<Product, T> GetDictionary<T>(this ISession session, Product key)
        {
            var value = session.GetString(key.ToString());

            return value == null ? new Dictionary<Product, T>() :
                JsonConvert.DeserializeObject<Dictionary<Product, T>>(value);
        }
    }
}
