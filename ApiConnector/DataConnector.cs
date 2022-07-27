using Newtonsoft.Json;
using System.Text;
using WebApi.Models;

namespace ApiConnector
{
    public class DataConnector
    {
        static HttpClient client = new HttpClient();

        public static List<DataModel>? GetData(string path) =>
            JsonConvert.DeserializeObject<List<DataModel>>
                (client.GetAsync(path).Result.Content.ReadAsStringAsync().Result);

        public static DataModel CreateData(string path, DataModel dataModel) =>
            Task.FromResult(GetDataModelInstanceFromResponse(client.PostAsync(path,
                GetStringContentFromDataModel(dataModel)).GetAwaiter().GetResult()).Result).Result;

        public static DataModel UpdateData(string path, DataModel dataModel) =>
            Task.FromResult(GetDataModelInstanceFromResponse(client.PutAsync(path,
                GetStringContentFromDataModel(dataModel)).GetAwaiter().GetResult()).Result).Result;

        public static HttpResponseMessage DeleteData(string path) =>
            client.DeleteAsync(path).GetAwaiter().GetResult();

        private static StringContent GetStringContentFromDataModel(DataModel subCategory) =>
            new StringContent(JsonConvert.SerializeObject(subCategory),
                    Encoding.UTF8,
                    "application/json");

        private static async Task<DataModel> GetDataModelInstanceFromResponse(HttpResponseMessage response)
        {
            DataModel? result = null;
            if (response.IsSuccessStatusCode)
            {
                string value = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<DataModel>(value);
            }

            if (result == null)
                throw new Exception("Connector error #2");
            return result;
        }
    }
}
