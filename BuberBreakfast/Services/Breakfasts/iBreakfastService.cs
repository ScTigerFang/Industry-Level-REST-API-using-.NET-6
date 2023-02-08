using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts
{
    public interface iBreakfastService
    {
        ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
        ErrorOr<Breakfast> GetBreakfast(Guid id);
        ErrorOr<UpsertedBreakfastResult> UpsertBreakfast(Breakfast breakfast);
        ErrorOr<Deleted> DeleteBreakfast(Guid id);
    }
}
