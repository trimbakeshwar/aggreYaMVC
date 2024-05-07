using aggreYaMVC.Models;
using aggreYaMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Diagnostics;

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
                    if (result.IsSuccess)
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
            ResponseModel Response = new ResponseModel();
            try
            {

                if (string.IsNullOrEmpty(LoginRequest.username) || string.IsNullOrEmpty(LoginRequest.password))
                {
                    return View("Login");
                }
                string message = "LOGIN SUCCESS";
                Response = await _accountServices.PostAsync<LoginModel>(_configuration["AccountEndPoint:Login"], LoginRequest);
                if (Response.IsSuccess)
                {
                    /*var token = _accountServices.GenerateToken(Register.Email);
                    ClaimsPrincipal claimsPrincipal = _accountServices.Validating(token);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                    });*/
                    // this.repoisitory.GenerateToken(Register.Email);
                    //GenerateTicket(Register.Email, ReturnUrl);
                    /*List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, Register.Email));
                    claims.Add(new Claim(ClaimTypes.Name, Register.Email));
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
*/

                    return Redirect("Customer/Index");
                    // return View(new { Status = true, Message = "Login Sucessfully", Data = result, token });
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
