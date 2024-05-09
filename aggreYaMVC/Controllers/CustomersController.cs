using aggreYaMVC.Models;
using aggreYaMVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.IO;
using static aggreYaMVC.Controllers.AccountController;

namespace aggreYaMVC.Controllers
{
    [Authorize(Roles = "HR")]
    public class CustomersController : Controller
    {

        private readonly IAccountServices _accountServices;
        private readonly ILogger<CustomersController> _logger;
        private readonly IConfiguration _configuration;
        public CustomersController(IAccountServices accountServices, ILogger<CustomersController> logger, IConfiguration configuration)
        {
            _accountServices = accountServices;
            _logger = logger;
            _configuration = configuration;
        }

        [BindProperty]
        public Customer customer { set; get; }
        [BindProperty]
        public List<Customer> CustomerList { set; get; }



   
        // GET: CustomersController
        public async Task<ActionResult> Index()
        {
            try
            {
               var  token = HttpContext.Session.GetString("JwtToken");
                var result = await _accountServices.GetAsync<Customer>(_configuration["AccountEndPoint:GetAllUser"], token).ConfigureAwait(false);
                var data = JsonConvert.DeserializeObject<GetUserResponseModel>(result);
                CustomerList = data.Results;
            }
            catch (Exception ex)
            {

            }
            return View(CustomerList);
        }

      

        // GET: CustomersController/Create

        [HttpGet]
        [ActionName("Create")]
        public async Task< ActionResult> GetCustomerById(int? id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                // var id = CreateModel.Id;

                if (id != 0 && id != null)
                {
                    var token = HttpContext.Session.GetString("JwtToken");
                    var result = await _accountServices.GetAsync<Customer>(_configuration["AccountEndPoint:GetUser"] + "/" + id, token);
                    var data = JsonConvert.DeserializeObject<GetUserByIDResponseModel>(result);
                    customer = data.Results;
                }

            }            
            catch (Exception ex)
            {

            }
            return View(customer);
        }
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                if (customer.Id != 0 && customer?.Id != null)
                { 
                var result =await  _accountServices.UpdateAsync<Customer>(_configuration["AccountEndPoint:UpdateUser"]+"/"+ customer.Id, customer, token);
                }
                else
                {

                    var result =await  _accountServices.PostAsync<Customer>(_configuration["AccountEndPoint:AddUser"], customer, token);
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");

          
        }

       

       

        // GET: CustomersController/Delete/5
        public async Task<ActionResult > Delete(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                if (id != 0)
                {
                    string message = "SUCCESS";
                    var result = await _accountServices.DeleteAsync<Customer>(_configuration["AccountEndPoint:DeleteUser"]+"/"+id, token);
                    
                    
                }
               
                    return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

     
       
    }
}
