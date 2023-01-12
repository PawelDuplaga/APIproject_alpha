using Breakfast.Models;
using ErrorOr;

namespace Breakfast.Services.Breakfast;

public interface IBreakfastService {
    public Task CreateBreakfast(Guid breakfast_id, BreakfastModel breakfastModel);
    public Task<ErrorOr<BreakfastModel>> GetBreakfast(Guid breakfast_id);
    public Task UpsertBreakfast(Guid breakfast_id, BreakfastModel breakfastModel);
    public Task DeleteBreakfast(Guid breakfast_id);


}