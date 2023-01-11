using Breakfast.Models;
using Google.Cloud.Firestore;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;


namespace Breakfast.Services.Breakfast;

public class BreakfastService : IBreakfastService
{
    private readonly Dictionary<Guid,BreakfastModel> _breakfasts = new Dictionary<Guid, BreakfastModel>();
    public void CreateBreakfast(BreakfastModel breakfastModel)
    {
        _breakfasts.Add(breakfastModel.Id,breakfastModel);
    }

    public BreakfastModel GetBreakfast(Guid id)
    {
        return _breakfasts[id];
    }


}