using aggreYaMVC.Models;
using aggreYaMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using static aggreYaMVC.Controllers.AccountController;

namespace aggreYaMVC.Controllers
{
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
                var result = await _accountServices.GetAsync<Customer>(_configuration["AccountEndPoint:GetAllUser"], "").ConfigureAwait(false);
                CustomerList = result.Results;
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
                     response = await _accountServices.GetAsync<Customer>(_configuration["AccountEndPoint:GetUser"] + "/" + id, "");
                    customer = response.Results;
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

                if (customer.Id != 0 && customer?.Id != null)
                {
                    var result =await  _accountServices.UpdateAsync<Customer>(_configuration["AccountEndPoint:UpdateUser"]+"/"+ customer.Id, customer, "");
                }
                else
                {

                    var result =await  _accountServices.PostAsync<Customer>(_configuration["AccountEndPoint:AddUser"], customer, "");
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
                if (id != 0)
                {
                    string message = "SUCCESS";
                    var result = await _accountServices.DeleteAsync<Customer>(_configuration["AccountEndPoint:DeleteUser"]+"/"+id, "");
                    
                    
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
