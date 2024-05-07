using System.ComponentModel.DataAnnotations;

namespace aggreYaMVC.Models
{

    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public dynamic? Results { get; set; }
    }
}
