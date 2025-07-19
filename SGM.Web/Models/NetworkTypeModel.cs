namespace SGM.Web.Models
{
    public class NetworkTypeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime creationDate { get; set; }
    }

    public class GetAllNetworkTypeResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public List<NetworkTypeModel> data { get; set; }
    }

    public class GetNetworkTypeResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public NetworkTypeModel data { get; set; }
    }

}
