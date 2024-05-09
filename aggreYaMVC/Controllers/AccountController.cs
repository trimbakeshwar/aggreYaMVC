using aggreYaMVC.Models;
using aggreYaMVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using NuGet.Common;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.IO;


namespace aggreYaMVC.Controllers
{
    public class AccountController : Controller
    {


        private readonly IAccountServices _accountServices;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountServices accountServices, ILogger<AccountController> logger, IConfiguration configuration)
        {
            _accountServices = accountServices;
            _logger = logger;
            _configuration = configuration;
        }

        [BindProperty]
        public Registration Register { set; get; }
        [BindProperty]
        public string role { set; get; }
        [BindProperty]
        public List<Registration> RegistrationList { set; get; }

        [BindProperty]
        public string Message { set; get; }


        public IActionResult Index()
        {
            return View("Registerations");
        }

        public async Task<IActionResult> Registerations()
        {
            try
            {
                if (Register != null)
                {
                    string val = _configuration["AccountEndPoint:Register"];
                    string message = "SUCCESS";
                    RegisterViewModel request = new RegisterViewModel();
                    request.registration = Register;
                    request.role = role;
                    var result = await _accountServices.PostAsync<RegisterViewModel>(_configuration["AccountEndPoint:Register"], request);
                    ResponseModel Response = JsonConvert.DeserializeObject<ResponseModel>(result);
                    if (Response.IsSuccess)
                    {
                        return View("Login");

                    }

                }
                Message = "Registration Fail";
                return View("Registerations");

            }
            catch (Exception ex)
            {
                Message = "Registration Fail";
                return View("Registrations");
            }
        }
        public IActionResult Login(LoginModel LoginRequest, string ReturnUrl)
        {
            ViewData["ReturnedUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost("Account/Login")]
        public async Task<IActionResult> Verify(LoginModel LoginRequest, string ReturnUrl)
        {
            LoginResponseModel Response = new LoginResponseModel();
            try
            {

                if (string.IsNullOrEmpty(LoginRequest.username) || string.IsNullOrEmpty(LoginRequest.password))
                {
                    return View("Login");
                }
               
                string message = "LOGIN SUCCESS";
                string result = await _accountServices.PostAsync<LoginModel>(_configuration["AccountEndPoint:Login"], LoginRequest);
                 Response = JsonConvert.DeserializeObject<LoginResponseModel>(result);
                if (Response.IsSuccess)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = _configuration["JWT:ValidAudience"],
                        ValidIssuer = _configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))

                    };
                    HttpContext.Session.SetString("JwtToken", Response.Results.token);

                    ClaimsPrincipal claimsPrincipal = GetClaimPrinciple(Response.Results.token);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = Response.Results.expiration
                    });
                    return Redirect("~/Customers/Index");
                }
                Message = "Login Fail";
                return View("Login");
            }
            catch (Exception ex)
            {
                Message = "Login Fail";
                return View("Login");
            }
        }
        [NonAction]
        public ClaimsPrincipal GetClaimPrinciple(string token)
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))

                };

                 principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

               
            }
            catch (Exception ex)
            {
               

            }
            return principal;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
