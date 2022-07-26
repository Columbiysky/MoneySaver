using Newtonsoft.Json;
using System.Text;
using WebApi.Models;

namespace ApiConnector
{
    public class CategoryConnector
    {
        static HttpClient client = new HttpClient();

        public static List<Category>? GetCategories(string path) =>
            JsonConvert.DeserializeObject<List<Category>>
                (client.GetAsync(path).Result.Content.ReadAsStringAsync().Result);

        public static Category CreateCategory(string path, Category category) =>
            Task.FromResult(GetCategoryInstanceFromResponse(client.PostAsync(path,
                GetStringContentFromCategory(category)).GetAwaiter().GetResult()).Result).Result;

        public static Category UpdateCategory(string path, Category category) =>
            Task.FromResult(GetCategoryInstanceFromResponse(client.PutAsync(path,
                GetStringContentFromCategory(category)).GetAwaiter().GetResult()).Result).Result;

        public static HttpResponseMessage DeleteCategory(string path) =>
             client.DeleteAsync(path).GetAwaiter().GetResult();


        private static StringContent GetStringContentFromCategory(Category category) =>
            new StringContent(JsonConvert.SerializeObject(category),
                    Encoding.UTF8,
                    "application/json");

        private static async Task<Category> GetCategoryInstanceFromResponse(HttpResponseMessage response)
        {
            Category? result = null;
            if (response.IsSuccessStatusCode)
            {
                string value = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Category>(value);
            }

            if (result == null)
                throw new Exception("Connector error #2");
            return result;
        }
    }
}
