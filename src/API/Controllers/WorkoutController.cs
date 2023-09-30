using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetWorkouts()
        {
            return Ok();
        }
    }
}
