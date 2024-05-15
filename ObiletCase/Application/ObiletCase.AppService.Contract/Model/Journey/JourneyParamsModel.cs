using System;
namespace ObiletCase.AppService.Contract.Model.Journey
{
    public class JourneyParamsModel
    {
        public string origin { get; set; }
        public string destination { get; set; }
        public string date { get; set; }
        public string sessionId { get; set; }
        public string deviceId { get; set; }
    }
}

