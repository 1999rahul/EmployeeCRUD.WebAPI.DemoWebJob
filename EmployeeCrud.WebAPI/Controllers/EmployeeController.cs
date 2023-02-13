using EmployeeCrud.Data;
using EmployeeCrud.Domain.Models;
using EmployeeCrud.Services.Iservices;
using EmployeeCrud.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace EmployeeCrud.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class EmployeeController : ControllerBase
    {
        public IEmployeeService employeeService;
        public EmployeeDBContext _context;
        public IConfiguration configuration;
        public EmployeeController(IEmployeeService employeeService, EmployeeDBContext context, IConfiguration configuration)
        {
            this.employeeService = employeeService;
            _context = context;            this.configuration = configuration;

        }
        [HttpGet("GetEmployee/{id}")]
        public IActionResult getEmployee(int id)
        {
           
            var response = employeeService.GetEmployee(id);
            return Ok(response);

        }
        [HttpGet("GeAllEmployee")]
        public IActionResult GeAllEmployee()
        {
            var response = employeeService.GetAllEmployees();
            return Ok(response);
        }
        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody] EmployeeVM employeeVM)
        {
            

          
            var response = employeeService.PostEmployee(employeeVM);
            var currVal = employeeService.GetEmployee(response.Data.EmployeeId);
            var data = new AuditEmployeeData()
            {
                PreviousValue = null,
                CurrentValue = JsonConvert.SerializeObject(currVal.Data),
                Operation = "INSERT"
            };
            AddToAuditQueue(data);
            return Ok(response);
        }

        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeVM employeeVM)
        {
            var prevValue = employeeService.GetEmployee(employeeVM.EmployeeId);
            var response = employeeService.UpdateEmployee(employeeVM);
            var data = new AuditEmployeeData()
            {
                PreviousValue= JsonConvert.SerializeObject(prevValue.Data),
                CurrentValue= JsonConvert.SerializeObject(response.Data),
                Operation="UPDATE"
            };
            AddToAuditQueue(data);
            return Ok(response);
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var prevValue = employeeService.GetEmployee(id);
            var data = new AuditEmployeeData()
            {
                PreviousValue = JsonConvert.SerializeObject(prevValue.Data),
                CurrentValue = null,
                Operation = "DELETE"
            };
            AddToAuditQueue(data);
            var response = employeeService.DeleteEmployee(id);
            return Ok(response);
        }

        private void AddToAuditQueue(AuditEmployeeData data)
        {

            var connString = configuration.GetSection("ConnectionStrings:storageAccountConnection").Value;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connString);
            CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference("audit-queue");
            CloudQueueMessage queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(data));
            cloudQueue.AddMessageAsync(queueMessage);
        }
    }
}
