using Breakfast.Models;

namespace Breakfast.Services.Breakfast;

public interface IBreakfastService {
    void CreateBreakfast(BreakfastModel breakfast, Guid obj_Id);
    BreakfastModel GetBreakfast(Guid id);
}