using System.Text.Json.Serialization;

namespace CapitalTask.Models
{
    public class Response
    {
        public Response()
        {

        }
        public Response(bool success, string msg)
        {
            Success = success;
            Msg = msg;
        }
        public Response(bool success, string msg, object data)
        {
            Success = success;
            Msg = msg;
            this.data = data;
        }
        public bool Success { get; set; }
        public string Msg { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? data { get; set; }
    }
}