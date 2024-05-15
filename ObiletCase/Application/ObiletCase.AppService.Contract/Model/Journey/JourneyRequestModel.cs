using System;
using Newtonsoft.Json;

namespace ObiletCase.AppService.Contract.Model.Journey
{
    public class Data
    {
        [JsonProperty("origin-id")]
        public int originid { get; set; }

        [JsonProperty("destination-id")]
        public int destinationid { get; set; }

        [JsonProperty("departure-date")]
        public string departuredate { get; set; }
    }

    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string sessionid { get; set; }

        [JsonProperty("device-id")]
        public string deviceid { get; set; }
    }

    public class JourneyRequestModel
    {
        [JsonProperty("device-session")]
        public DeviceSession devicesession { get; set; } = new DeviceSession();
        public string date { get; set; } = "2021-09-01";
        public string language { get; set; } = "tr-TR";
        public Data data { get; set; } = new Data();
    }
}

