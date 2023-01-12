using Breakfast.Models;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;


namespace Breakfast.Services.Breakfast;

public class BreakfastService : IBreakfastService
{
    FireBaseService fireBaseService;
    private static readonly Dictionary<Guid,BreakfastModel> _breakfasts = new Dictionary<Guid, BreakfastModel>();

    public BreakfastService()
    {
        fireBaseService = new FireBaseService();
    }

    public void CreateBreakfast(BreakfastModel breakfastModel, Guid breakfast_id)
    {
        _breakfasts.Add(breakfastModel.Id,breakfastModel);
        fireBaseService.Create("Breakfast/",breakfastModel,breakfast_id);
    }


    public BreakfastModel GetBreakfast(Guid id)
    {
        return _breakfasts[id];
    }
}


