using Breakfast.Conctracts.Breakfast;
using Microsoft.AspNetCore.Mvc;
using Breakfast.Models;
using Breakfast.Services.Breakfast;

namespace Breakfast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreakfastController : ControllerBase
    {

        private readonly IBreakfastService _breakfastService;

        public BreakfastController(IBreakfastService breakfastService)
        {
            _breakfastService = breakfastService;
        }


        [HttpPost]
        public IActionResult CreateBreakfast(CreateBreakfastRequest request)
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

            // TODO: save breakfast to database
            _breakfastService.CreateBreakfast(breakfast.Id, breakfast);

            var response = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet
            );

            return CreatedAtAction(
                actionName: nameof(GetBreakfast),
                routeValues : new { id = breakfast.Id},
                value: response
            );
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            BreakfastModel breakfast = _breakfastService.GetBreakfast(id);

            var response = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet
            );
            
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateBreakfast(Guid Id, UpsertBreakfastRequest request)
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

            _breakfastService.UpsertBreakfast(breakfast.Id, breakfast);
            
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id)
        {
            _breakfastService.DeleteBreakfast(id);
            return NoContent();
        }

    }
}
