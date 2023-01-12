using Breakfast.Conctracts.Breakfast;
using Microsoft.AspNetCore.Mvc;
using Breakfast.Models;
using Breakfast.Services.Breakfast;
using Breakfast.Controllers;
using ErrorOr;

namespace Breakfast.Controllers
{
    
    public class BreakfastController : ApiController
    {
        private readonly IBreakfastService _breakfastService;

        public BreakfastController(IBreakfastService breakfastService)
        {
            _breakfastService = breakfastService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBreakfast(CreateBreakfastRequest request)
        {           
            //Could first use _breakfastService to save data to Firebase db to get the key of the new written
            //data in response from db and then attached it to API response
            var breakfast = new BreakfastModel(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                DateTime.UtcNow,
                request.Savory,
                request.Sweet);

            ErrorOr<Created> createBreakfastResult =  await _breakfastService.CreateBreakfast(breakfast.Id, breakfast);

            return createBreakfastResult.Match(
                created => CreatedAsGetBreakfast(breakfast),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBreakfast(Guid id)
        {
            ErrorOr<BreakfastModel> getBreakfastResult = await _breakfastService.GetBreakfast(id);

            return getBreakfastResult.Match(
                breakfast => Ok(MapBreakfastResponse(breakfast)),
                errors => Problem(errors));

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpsertBreakfast(Guid Id, UpsertBreakfastRequest request)
        {
            var breakfast = new BreakfastModel(
                Id,
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                DateTime.UtcNow,
                request.Savory,
                request.Sweet);

            ErrorOr<UpsertedBreakfast> upsertedBreakfastResult = await _breakfastService.UpsertBreakfast(breakfast.Id, breakfast);

            return upsertedBreakfastResult.Match(
                upserted => upserted.isNewlyCreated ? CreatedAsGetBreakfast(breakfast) : NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBreakfast(Guid id)
        {
            ErrorOr<Deleted> deleteBreakfastResult = await _breakfastService.DeleteBreakfast(id);
            return deleteBreakfastResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
            );
        }

        private static BreakfastResponse MapBreakfastResponse(BreakfastModel breakfast)
        {
            return new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet
            );
        }

        private CreatedAtActionResult CreatedAsGetBreakfast(BreakfastModel breakfast)
        {
             return CreatedAtAction(
                actionName: nameof(GetBreakfast),
                routeValues : new { id = breakfast.Id},
                value: MapBreakfastResponse(breakfast)
            );
        }

    }
}
