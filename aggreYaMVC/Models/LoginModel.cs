using Microsoft.VisualBasic;
using System.Text.Json.Serialization;

namespace aggreYaMVC.Models
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class LoginResponseModel
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("results")]
        public TokenModel? Results { get; set; }
    }
    public class TokenModel
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
