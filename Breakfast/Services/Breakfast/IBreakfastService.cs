using Breakfast.Models;
using ErrorOr;

namespace Breakfast.Services.Breakfast;

public interface IBreakfastService {
    public void CreateBreakfast(Guid breakfast_id, BreakfastModel breakfastModel);
    public ErrorOr<BreakfastModel> GetBreakfast(Guid breakfast_id);
    public void UpsertBreakfast(Guid breakfast_id, BreakfastModel breakfastModel);
    public void DeleteBreakfast(Guid breakfast_id);


}