using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryApi.Controllers
{
    public class StatusController : Controller
    {
        ISystemTime systemTime;

        public StatusController(ISystemTime systemTime)
        {
            this.systemTime = systemTime;
        }

        // GET /status - > 200 OK
        [HttpGet("/status")]
        public ActionResult GetTheStatus()
        {
            var response = new StatusResponse
            {
                Message = "This is great stuff",
                CheckedBy = "Joe Blow",
                WhenLastChecked = systemTime.GetCurrentTime()
            };

            return Ok(response);
        }

        [HttpGet("employees/{employeeId:int:min(0)}/salary")]
        public ActionResult GetSalary(int employeeId)
        {
            return Ok($"The Employee {employeeId} has a salary of $72,000");
        }

        [HttpGet("employees")]
        public ActionResult GetEmployees([FromQuery]string dept = "All")
        {
            return Ok($"Returning employees for department {dept}");
        }


        public class StatusResponse
        {
            public string Message { get; set; }
            public string CheckedBy { get; set; }
            public DateTime WhenLastChecked { get; set; }
        }
    }
}
