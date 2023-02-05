using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;

namespace BuberBreakfast.Services.Breakfasts
{
    public interface iBreakfastService
    {
        void CreateBreakfast(Breakfast breakfast);
        Breakfast GetBreakfast(Guid id);
        //BreakfastResponse DeleteBreakfast(Guid id);
        //BreakfastResponse GetBreakfast(Guid id);
        //BreakfastResponse UpsertBreakfast(Guid id, UpsertBreakfastRequest request);
    }
}
