namespace SGM.Web.Models
{
    public class NetworkTypeEditModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime updateAt { get; set; }

    }
    public class NetworkTypeEditResponse 
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
