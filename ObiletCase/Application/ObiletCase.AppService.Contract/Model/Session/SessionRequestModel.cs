using Newtonsoft.Json;

namespace ObiletCase.AppService.Contract.Model.Session
{
    public class Connection
    {
        [JsonProperty("ip-address")]
        public string ipaddress { get; set; } = "165.114.41.21";
        public string port { get; set; } = "5117";
    }

    public class Browser
    {
        public string name { get; set; } = "Chrome";
        public string version { get; set; } = "47.0.0.12";
    }

    public class SessionRequestModel
    {
        public int type { get; set; } = 1;
        public Connection connection { get; set; } = new Connection();
        public Browser browser { get; set; } = new Browser();
    }
}

