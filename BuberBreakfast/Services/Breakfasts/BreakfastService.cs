using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts
{
    public class BreakfastService : iBreakfastService
    {
        //Currently storing in memory, can replace with a database of some sort or internal storage
        private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();

        public void CreateBreakfast(Breakfast breakfast)
        {
            _breakfasts.Add(breakfast.Id, breakfast);
        }

        public ErrorOr<Breakfast> GetBreakfast(Guid id)
        {
            if (_breakfasts.TryGetValue(id, out var breakfast))
            {
                return breakfast;
            }
            return Errors.Breakfast.NotFound;
        }

        public void DeleteBreakfast(Guid id) { 
            _breakfasts.Remove(id);
        }

        public void UpsertBreakfast(Breakfast breakfast)
        {
            _breakfasts[breakfast.Id] = breakfast;
        }
        
    }
}
