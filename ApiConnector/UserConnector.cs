using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Models;
using Formatting = Newtonsoft.Json.Formatting;

namespace ApiConnector
{
    public static class UserConnector
    {
        static HttpClient client = new HttpClient();

        public static List<User> GetAllUsers(string path) =>
            JsonConvert.DeserializeObject<List<User>>(GetAllClientsString(path)) ??
                throw new Exception("Something went wrong");

        public static string GetAllClientsString(string path) =>
            client.GetAsync(path)
                .Result.Content
                .ReadAsStringAsync()
                .Result;

        public static async Task<User> GetUser(string path) =>
            GetUserInstanceFromResponse(await client.GetAsync(path)).Result;

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
            newUser.Pass = CryptPass(newUser.Pass ?? throw new Exception("Connector error #5"));
            return GetUserInstanceFromResponse(await client.PutAsync(path,
                new StringContent(JsonConvert.SerializeObject(newUser),
                Encoding.UTF8,
                "application/json"))).Result;
        }

        private static string CryptPass(string pass)
        {
            using (var crypter = SHA512.Create())
            {
                var schiferData = crypter.ComputeHash(Encoding.ASCII.GetBytes(pass ?? throw new Exception("Connector error #3")));
                var sBuilder = new StringBuilder();
                foreach (var element in schiferData)
                    sBuilder.Append(element.ToString("x2"));

                return sBuilder.ToString();
            }
        }

        private static async Task<User> GetUserInstanceFromResponse(HttpResponseMessage response)
        {
            User? result = null;
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