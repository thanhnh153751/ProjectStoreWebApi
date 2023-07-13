using Newtonsoft.Json;

namespace eBookStore.ResponseModel
{
    public class ODataResponse<T>
    {
        [JsonProperty("@odata.context")]
        public string Context { get; set; }

        [JsonProperty("@odata.count")]
        public int Count { get; set; }

        [JsonProperty("value")]
        public List<T> Value { get; set; }
    }

}
