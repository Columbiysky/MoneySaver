using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ApiConnector
{
    public class SubCategoryConnector
    {
        static HttpClient client = new HttpClient();

        public static List<SubCategory>? GetSubCategories(string path) =>
            JsonConvert.DeserializeObject<List<SubCategory>>
                (client.GetAsync(path).Result.Content.ReadAsStringAsync().Result);

        public static SubCategory CreateSubCategory(string path, SubCategory subCategory) =>
            Task.FromResult(GetSubCategoryInstanceFromResponse(client.PostAsync(path,
                GetStringContentFromSubCategory(subCategory)).GetAwaiter().GetResult()).Result).Result;

        public static SubCategory UpdateSubCategory(string path, SubCategory subCategory) =>
            Task.FromResult(GetSubCategoryInstanceFromResponse(client.PutAsync(path,
                GetStringContentFromSubCategory(subCategory)).GetAwaiter().GetResult()).Result).Result;

        public static HttpResponseMessage DeleteSubCategory(string path) =>
            client.DeleteAsync(path).GetAwaiter().GetResult();

        private static StringContent GetStringContentFromSubCategory(SubCategory subCategory) =>
            new StringContent(JsonConvert.SerializeObject(subCategory),
                    Encoding.UTF8,
                    "application/json");

        private static async Task<SubCategory> GetSubCategoryInstanceFromResponse(HttpResponseMessage response)
        {
            SubCategory? result = null;
            if (response.IsSuccessStatusCode)
            {
                string value = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<SubCategory>(value);
            }

            if (result == null)
                throw new Exception("Connector error #2");
            return result;
        }
    }
}
