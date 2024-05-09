using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace aggreYaMVC.Models
{
    public class CommonModel
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
    public class ResponseModel : CommonModel
    {
        
        [JsonPropertyName("results")]
        public dynamic? Results { get; set; }
    }
    public class GetUserResponseModel : CommonModel
    {
     
        [JsonPropertyName("results")]
        public List<Customer>? Results { get; set; }
    }
    public class GetUserByIDResponseModel : CommonModel
    {

        [JsonPropertyName("results")]
        public Customer? Results { get; set; }
    }
}
