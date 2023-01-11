using Breakfast.Models;

namespace Breakfast.Services.Breakfast;

public interface IBreakfastService {
    void CreateBreakfast(BreakfastModel breakfast);
    BreakfastModel GetBreakfast(Guid id);
}