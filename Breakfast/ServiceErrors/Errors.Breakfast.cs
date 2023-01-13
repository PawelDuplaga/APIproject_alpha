using ErrorOr;

namespace Breakfast.ServiceErrors;

public static class Errors
{

    public static class Syntax
    {
        public static Error GuidBadSyntax => Error.Validation(
            code: "Guid.BadSyntax",
            description: "Given id have wrong syntax"
        );
    }


    public static class Breakfast
    {
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found"
        );

        public static Error Unexpected => Error.Unexpected(
            code: "Breakfast.Unexpected",
            description: "Unexpected Error occured"
        );
    }


}