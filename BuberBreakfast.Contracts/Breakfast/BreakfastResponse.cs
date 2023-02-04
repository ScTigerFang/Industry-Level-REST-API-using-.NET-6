namespace BuberBreakfast.Contracts.Breakfast
{
    public record BreakfastResponse
    (
        Guid Guid,
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        DateTime LastModifiedDateTime,
        List<string> Savory,
        List<string> Sweet
    );

}
