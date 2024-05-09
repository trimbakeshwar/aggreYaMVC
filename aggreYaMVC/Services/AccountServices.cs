using aggreYaMVC.Models;
using aggreYaMVC.Repo;
using System.Security.Claims;

namespace aggreYaMVC.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepo _accountRepo;
        public AccountServices(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public string EncryptPassword(string password)
        {
            return _accountRepo.EncryptPassword(password);
        }
        public async Task<string> PostAsync<T>(string RequestUri, T Request)
        {
            return await _accountRepo.PostAsync<T>(RequestUri, Request);
        }
        public async Task<ResponseModel> PostAsync<T>(string RequestUri, T Request, string Token)
        {
            return await _accountRepo.PostAsync<T>(RequestUri, Request, Token);
        }
        public async Task<ResponseModel> UpdateAsync<T>(string RequestUri, T Request, string Token)
        {
            return await _accountRepo.UpdateAsync<T>(RequestUri, Request, Token);
        }
        public async Task<ResponseModel> DeleteAsync<T>(string RequestUri,  string Token)

        {
            return await _accountRepo.DeleteAsync<T>(RequestUri, Token);
        }
        public async Task<string> GetAsync<T>(string RequestUri, string Token)

        {
            return await _accountRepo.GetAsync<T>(RequestUri, Token);
        }

        public bool SendEmail(string emailAddress)
        {
            return _accountRepo.SendEmail(emailAddress);
        }
    }
}
