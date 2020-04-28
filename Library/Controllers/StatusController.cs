using Microsoft.AspNetCore.Mvc;
using System;

namespace Library.Controllers
{
    public class StatusController : Controller
    {
        // GET /status - > 200 OK
        [HttpGet("/status")]
        public ActionResult GetTheStatus()
        {
            var response = new StatusResponse
            {
                Message = "This is great stuff",
                CheckedBy = "Joe Blow",
                CheckedTimeStamp = DateTime.Now
            };

            return Ok(response);
        }


        public class StatusResponse
        {
            public string Message { get; set; }
            public string CheckedBy { get; set; }
            public DateTime CheckedTimeStamp { get; set; }
        }
    }
}
