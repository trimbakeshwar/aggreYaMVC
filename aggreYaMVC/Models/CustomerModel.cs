using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace aggreYaMVC.Models
{
    public class Customer
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { set; get; }
        [JsonPropertyName("firstName")]
        [RegularExpression(@"^[A-Z][a-z]{2,}$", ErrorMessage = "Please enter a valid first Name")]

        public string? FirstName { set; get; }
        [JsonPropertyName("lastName")]
        [RegularExpression(@"^[A-Z][a-z]{2,}$", ErrorMessage = "Please enter a valid Last Name")]
        public string? LastName { set; get; }
        [JsonPropertyName("password")]
        [RegularExpression(@"^.*(?=.*[A-Z])*(?=.*[0-9])*(?=.*[a-z])*(?=.*[!@#$%^&*_+]{1})(.{8,})$", ErrorMessage = "Please enter a valid password")]
        public string? Password { set; get; }
        [JsonPropertyName("loginUser")]
        [RegularExpression(@"^[a-z0-9_-]{3,15}$", ErrorMessage = "Please enter a valid user Name")]
        public string? LoginUser { set; get; }
        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        public string? Email { set; get; }
        [JsonPropertyName("phoneNumber")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter a valid Mobile Number")]
        public string? PhoneNumber { set; get; }
    }
}
