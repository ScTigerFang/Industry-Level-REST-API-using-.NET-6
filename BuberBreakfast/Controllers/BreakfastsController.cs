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

            ErrorOr<Created> createdBreakfastResult = _breakfastService.CreateBreakfast(breakfast);


            return createdBreakfastResult.Match(
                created => CreatedAtGetBreakfast(breakfast),
                errors => Problem(errors));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

            return getBreakfastResult.Match(
                    breakfast => Ok(MapBreakfastResponse(breakfast)),
                    errors => Problem(errors)
                );
            #region possible other way
            //if (getBreakfastResult.IsError && getBreakfastResult.FirstError == Errors.Breakfast.NotFound)
            //{
            //    return NotFound();
            //}

            //var breakfast = getBreakfastResult.Value;

            //BreakfastResponse response = MapBreakfastResponse(breakfast);

            //return Ok(response);
            #endregion
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

            ErrorOr<UpsertedBreakfastResult> upsertBreakFastResult = _breakfastService.UpsertBreakfast(breakfast);
            return upsertBreakFastResult.Match(
                upserted => upserted.isNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id) {
            ErrorOr<Deleted> deleteBreakfastResults = _breakfastService.DeleteBreakfast(id);

            return deleteBreakfastResults.Match(
                deleted => NoContent(),
                errors => Problem(errors));
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

        private CreatedAtActionResult CreatedAtGetBreakfast(Breakfast breakfast)
        {
            return CreatedAtAction(
                        actionName: nameof(GetBreakfast),
                        routeValues: new { id = breakfast.Id },
                        value: MapBreakfastResponse(breakfast));
        }
    }
}
