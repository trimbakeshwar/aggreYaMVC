using aggreYaMVC.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace aggreYaMVC.Services
{
    public interface IAccountServices
    {
        /*string Registerations(Registration register);
        Registration getUserDetails(string email);*/
        Task<ResponseModel> PostAsync<T>(string RequestUri, T Request);
        Task<ResponseModel> PostAsync<T>(string RequestUri, T Request, string Token);
        Task<ResponseModel> UpdateAsync<T>(string RequestUri, T Request, string Token);
         Task<ResponseModel> GetAsync<T>(string RequestUri, string Token);
        Task<ResponseModel> DeleteAsync<T>(string RequestUri, string Token);
        string EncryptPassword(string password);
        /*string Login(string Email, string Password);
        IEnumerable<Registration> GetUser();
        string GenerateToken(string userEmail);
        ClaimsPrincipal Validating(string token);*/
        bool SendEmail(string emailAddress);
         
        
      
    }
}
