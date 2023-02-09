using ErrorOr;

namespace BuberBreakfast.ServiceErrors
{
    public static class Errors
    {
        public static class Breakfast
        {
            public static Error Invalidname => Error.Validation(
                code: "Breakfast.Invalid Name",
                description: $"Breakfast name must be atleast {Models.Breakfast.MinNameLength}" +
                    $" characters long at most {Models.Breakfast.MaxNameLength}");
            public static Error NotFound => Error.NotFound(
                code: "Breakfast.NotFOund",
                description: "Breakfast Not Found");

            public static Error InvalidDescription => Error.Validation(
                code: "Breakfast.Invalid Description",
                description: $"Breakfast Description must be atleast {Models.Breakfast.MinDescriptionLength}" +
                    $" characters long at most {Models.Breakfast.MaxDescriptionLength}");
        }
    }
}
