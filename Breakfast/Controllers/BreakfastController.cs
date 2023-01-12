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

            ErrorOr<Created> CreateBreakfastResult =  await _breakfastService.CreateBreakfast(breakfast.Id, breakfast);
            if(CreateBreakfastResult.IsError)
            {
                return Problem(CreateBreakfastResult.Errors);
            }

            return CreatedAtAction(
                actionName: nameof(GetBreakfast),
                routeValues : new { id = breakfast.Id},
                value: MapBreakfastResponse(breakfast)
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

            ErrorOr<Updated> upsertedResult = await _breakfastService.UpsertBreakfast(breakfast.Id, breakfast);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBreakfast(Guid id)
        {
            ErrorOr<Deleted> deletedResult = await _breakfastService.DeleteBreakfast(id);
            return deletedResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
            );
        }

           public static BreakfastResponse MapBreakfastResponse(BreakfastModel breakfast)
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

    }
}
