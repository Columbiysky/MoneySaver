using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Models;
using Formatting = Newtonsoft.Json.Formatting;

namespace ApiConnector
{
    public static class UserConnector
    {
        static HttpClient client = new HttpClient();
        static SHA512 crypter = new SHA512Managed(); 

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

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }

        public static List<User> GetAllUsers(string path) =>
            JsonConvert.DeserializeObject<List<User>>(GetAllClientsString(path)) ??
                throw new Exception("Something went wrong");

        public static string GetAllClientsString(string path) =>
            JsonPrettify(client.GetAsync(path)
                .Result.Content
                .ReadAsStringAsync()
                .Result);

        public static async Task<User> GetUser(string path) =>
            GetUserInstanceFromResponse(client.GetAsync(path).Result).Result;

        public static async Task<User> CreateUser(string path, User user)
        {
            user.Pass = CryptPass(user.Pass ?? throw new Exception("Connector error #4"));
            return GetUserInstanceFromResponse(await client.PostAsync(path,
                new StringContent(JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json"))).Result;
        }

        public static async Task<User> UpdateUser(string path, User newUser)
        {
            //User oldUser = GetUser(path+$"?Login={newUser.Login}").Result;
            //oldUser = newUser;
            newUser.Pass = CryptPass(newUser.Pass ?? throw new Exception("Connector error #5"));
            return GetUserInstanceFromResponse(await client.PutAsync(path,
                new StringContent(JsonConvert.SerializeObject(newUser),
                Encoding.UTF8,
                "application/json"))).Result;
        }

        private static string CryptPass(string pass)
        {
            var schiferData = crypter.ComputeHash(Encoding.ASCII.GetBytes(pass ?? throw new Exception("Connector error #3")));
            var sBuilder = new StringBuilder();
            foreach (var element in schiferData)
                sBuilder.Append(element.ToString("x2"));

            return sBuilder.ToString();
        }

        private static async Task<User> GetUserInstanceFromResponse(HttpResponseMessage response)
        {
            User result = null;
            if (response.IsSuccessStatusCode)
            {
                string value = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<User>(value);
            }

            if (result == null)
                throw new Exception("Connector error #2");
            return result;
        }
    }
}