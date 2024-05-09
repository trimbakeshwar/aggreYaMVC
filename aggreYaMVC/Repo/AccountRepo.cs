using aggreYaMVC.Models;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace aggreYaMVC.Repo
{
    public class AccountRepo : IAccountRepo
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AccountRepo> _logger;
        public AccountRepo(HttpClient httpClient, ILogger<AccountRepo> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> PostAsync<T>(string RequestUri, T Request)
        {

            HttpResponseMessage response = new HttpResponseMessage();

            string Reqcontent = System.Text.Json.JsonSerializer.Serialize(Request);

          _httpClient.DefaultRequestHeaders.Clear();
            /*   _httpClient.DefaultRequestHeaders.Add("Authorization", Token);*/

            var _content = new StringContent(Reqcontent, Encoding.UTF8, "application/json");
            /*_content.Headers.TryAddWithoutValidation("user_id",userid);*/
           

                response = await _httpClient.PostAsync(RequestUri, _content).ConfigureAwait(false);

            
            var stream = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            

            if (response.IsSuccessStatusCode)
            {
                return stream;
            }

            var streamerr = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var content = await StreamToStringAsync(streamerr).ConfigureAwait(false);
            var apiException = new TransportException
            {
                StatusCode = (int)response.StatusCode,
                Content = content
            };

            throw apiException;
        }
        public async Task<ResponseModel> PostAsync<T>(string RequestUri, T Request, string Token)
        {

            HttpResponseMessage response = new HttpResponseMessage();

            string Reqcontent = System.Text.Json.JsonSerializer.Serialize(Request);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            var _content = new StringContent(Reqcontent, Encoding.UTF8, "application/json");
            /*_content.Headers.TryAddWithoutValidation("user_id",userid);*/


            response = await _httpClient.PostAsync(RequestUri, _content).ConfigureAwait(false);


            var stream = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<ResponseModel>(stream);

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            var streamerr = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var content = await StreamToStringAsync(streamerr).ConfigureAwait(false);
            var apiException = new TransportException
            {
                StatusCode = (int)response.StatusCode,
                Content = content
            };

            throw apiException;
        }
        public async Task<ResponseModel> UpdateAsync<T>(string RequestUri, T Request, string Token)
        {

            HttpResponseMessage response = new HttpResponseMessage();

            string Reqcontent = System.Text.Json.JsonSerializer.Serialize(Request);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);


            var _content = new StringContent(Reqcontent, Encoding.UTF8, "application/json");
            /*_content.Headers.TryAddWithoutValidation("user_id",userid);*/


            response = await _httpClient.PutAsync(RequestUri, _content).ConfigureAwait(false);


            var stream = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<ResponseModel>(stream);

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            var streamerr = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var content = await StreamToStringAsync(streamerr).ConfigureAwait(false);
            var apiException = new TransportException
            {
                StatusCode = (int)response.StatusCode,
                Content = content
            };

            throw apiException;
        }
        public async Task<ResponseModel> DeleteAsync<T>(string RequestUri, string Token)
        {

            HttpResponseMessage response = new HttpResponseMessage();

          //  string Reqcontent = System.Text.Json.JsonSerializer.Serialize(Request);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);


            // var _content = new StringContent(Reqcontent, Encoding.UTF8, "application/json");
            /*_content.Headers.TryAddWithoutValidation("user_id",userid);*/


            response = await _httpClient.DeleteAsync(RequestUri).ConfigureAwait(false);


            var stream = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<ResponseModel>(stream);

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            var streamerr = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var content = await StreamToStringAsync(streamerr).ConfigureAwait(false);
            var apiException = new TransportException
            {
                StatusCode = (int)response.StatusCode,
                Content = content
            };

            throw apiException;
        }
        public async Task<string> GetAsync<T>(string RequestUri, string Token)
        {

            HttpResponseMessage response = new HttpResponseMessage();

            // string Reqcontent = System.Text.Json.JsonSerializer.Serialize(Request);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            // var _content = new StringContent(Reqcontent, Encoding.UTF8, "application/json");
            /*_content.Headers.TryAddWithoutValidation("user_id",userid);*/


            response = await _httpClient.GetAsync(RequestUri).ConfigureAwait(false);


            var stream = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //var result = JsonConvert.DeserializeObject<ResponseModel>(stream);

            if (response.IsSuccessStatusCode)
            {
                return stream;
            }

            var streamerr = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var content = await StreamToStringAsync(streamerr).ConfigureAwait(false);
            var apiException = new TransportException
            {
                StatusCode = (int)response.StatusCode,
                Content = content
            };

            throw apiException;
        }
        public  string EncryptPassword(string password)
        {
            try
            {
                byte[] encryptData = new byte[password.Length];
                encryptData = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encryptData);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public bool SendEmail(string emailAddress)
        {
            try
            {
               
                    return false;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }
    }
}
