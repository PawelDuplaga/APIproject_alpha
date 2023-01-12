using Breakfast.Models;
using ErrorOr;

namespace Breakfast.Services.Breakfast;

public interface IBreakfastService {
    public Task<ErrorOr<Created>> CreateBreakfast(Guid breakfast_id, BreakfastModel breakfastModel);
    public Task<ErrorOr<BreakfastModel>> GetBreakfast(Guid breakfast_id);
    public Task<ErrorOr<UpsertedBreakfast>> UpsertBreakfast(Guid breakfast_id, BreakfastModel breakfastModel);
    public Task<ErrorOr<Deleted>> DeleteBreakfast(Guid breakfast_id);


}