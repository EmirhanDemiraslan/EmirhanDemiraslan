using Newtonsoft.Json;

namespace ObiletCase.AppService.Contract.Model.Session
{
    public class Data
    {
        [JsonProperty("session-id")]
        public string sessionid { get; set; }

        [JsonProperty("device-id")]
        public string deviceid { get; set; }
    }

    public class SessionResponseModel
    {
        public string status { get; set; }
        public Data data { get; set; }
        public object message { get; set; }

        [JsonProperty("user-message")]
        public object usermessage { get; set; }

        [JsonProperty("api-request-id")]
        public object apirequestid { get; set; }
        public object controller { get; set; }
    }
}

