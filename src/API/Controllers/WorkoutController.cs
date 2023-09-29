using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        public IActionResult GetWorkouts()
        {
            return Ok();
        }
    }
}
