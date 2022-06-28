using Newtonsoft.Json;
using WebApi.Models;
using Formatting = Newtonsoft.Json.Formatting;

namespace ApiConnector
{
    public static class UserConnector
    {
        static HttpClient client = new HttpClient();

        // Prettify JSON without serialization
        // https://stackoverflow.com/a/30329731
        private static string JsonPrettify(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }

        public static List<User> GetAllClients(string path)
        {
            return JsonConvert.DeserializeObject<List<User>>(GetAllClientsString(path));
        }

        public static string GetAllClientsString(string path) => JsonPrettify(client.GetAsync(path)
                                                                        .Result.Content
                                                                        .ReadAsStringAsync()
                                                                        .Result);
    }
}