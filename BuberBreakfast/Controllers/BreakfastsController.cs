using Microsoft.AspNetCore.Mvc;
using BuberBreakfast.Contracts.Breakfast;

namespace BuberBreakfast.Controllers
{
    [ApiController]
    public class BreakfastsController : ControllerBase
    {
        [HttpPost("/breakfasts")]
        public IActionResult CreateBreakfast(CreateBreakfastRequest request)
        {
            return Ok(request);
        }

        [HttpGet("/breakfasts/{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            return Ok(id);
        }

        [HttpPut("/breakfasts/{id:guid}")]
        public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
        {
            return Ok(id);
        }

        [HttpDelete("/breakfasts/{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id) { 
            return Ok(id);
        }
    }
}
