using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers
{
    [ApiController]
    //[Route("breakfasts")]//one solution
    [Route("[controller]")]
    public class BreakfastsController : ApiController
    {
        private readonly iBreakfastService _breakfastService;

        public BreakfastsController(iBreakfastService breakfastService)
        {
            _breakfastService = breakfastService;
        }


        [HttpPost]
        public IActionResult CreateBreakfast(CreateBreakfastRequest request)
        {
            //return Ok();
            var breakfast = new Breakfast(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                DateTime.UtcNow,
                request.Savory,
                request.Sweet
                );

            _breakfastService.CreateBreakfast( breakfast );

            var response = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet);

            return CreatedAtAction(
                actionName: nameof(GetBreakfast),
                routeValues: new {id = breakfast.Id },
                value: response);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

            return getBreakfastResult.Match(
                    breakfast => Ok(MapBreakfastResponse(breakfast)),
                    errors => Problem(errors)
                ); 

            //if (getBreakfastResult.IsError && getBreakfastResult.FirstError == Errors.Breakfast.NotFound)
            //{
            //    return NotFound();
            //}

            //var breakfast = getBreakfastResult.Value;

            //BreakfastResponse response = MapBreakfastResponse(breakfast);

            //return Ok(response);
        }

        private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
        {
            return new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
        {
            var breakfast = new Breakfast(
                id,
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                DateTime.UtcNow,
                request.Savory,
                request.Sweet
                );

            _breakfastService.UpsertBreakfast(breakfast);
            // Return a 201 if a new breakfast was created
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id) {
            _breakfastService.DeleteBreakfast(id);
            return NoContent();
        }
    }
}
