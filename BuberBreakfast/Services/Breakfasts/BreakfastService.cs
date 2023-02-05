using BuberBreakfast.Models;

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

        public Breakfast GetBreakfast(Guid id)
        {
            return _breakfasts[id];
        }
    }
}
