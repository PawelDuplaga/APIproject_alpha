using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace Breakfast.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{

    protected IActionResult Problem(List<Error> errors)
    {
        var FirstError = errors[0];

        var StatusCode = FirstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        Console.WriteLine(StatusCode);
        Console.WriteLine(FirstError.Description);


        return Problem(statusCode: StatusCode, title: FirstError.Description);
    }

}