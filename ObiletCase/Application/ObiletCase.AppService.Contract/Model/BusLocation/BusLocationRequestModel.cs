using System;
using Newtonsoft.Json;

namespace ObiletCase.AppService.Contract.Model.BusLocation
{
    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string sessionid { get; set; }

        [JsonProperty("device-id")]
        public string deviceid { get; set; }
    }

    public class BusLocationRequestModel
    {
        public string data { get; set; }

        [JsonProperty("device-session")]
        public DeviceSession devicesession { get; set; } = new DeviceSession();
        public string date { get; set; } = "2016-03-11T11:33:00";
        public string language { get; set; } = "tr-TR";
    }
}

