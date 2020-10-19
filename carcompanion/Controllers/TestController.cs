using Microsoft.AspNetCore.Mvc;

namespace carcompanion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Works";
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value is " + id;
        }
    }
}