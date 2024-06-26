﻿using aggreYaMVC.Models;

namespace aggreYaMVC.Repo
{
    public interface IAccountRepo
    {
        Task<string> PostAsync<T>(string RequestUri, T Request);
        Task<ResponseModel> PostAsync<T>(string RequestUri, T Request, string Token);
        Task<ResponseModel> UpdateAsync<T>(string RequestUri, T Request, string Token);
        Task<ResponseModel> DeleteAsync<T>(string RequestUri, string Token);
        Task<string> GetAsync<T>(string RequestUri, string Token);
        string EncryptPassword(string password);
        bool SendEmail(string emailAddress);
    }
}
