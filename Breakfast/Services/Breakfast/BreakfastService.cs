using Breakfast.Models;
using Newtonsoft.Json;

namespace Breakfast.Services.Breakfast;

public class BreakfastService : IBreakfastService
{
    IFireBaseService fireBaseService;
    private static readonly Dictionary<Guid,BreakfastModel> _breakfasts = new Dictionary<Guid, BreakfastModel>();

    public BreakfastService()
    {
        fireBaseService = new FireBaseService();
    }

    public void CreateBreakfast(Guid breakfast_Id, BreakfastModel breakfastModel)
    {
        fireBaseService.Create("Breakfast/", breakfast_Id, breakfastModel);
    }

    public BreakfastModel GetBreakfast(Guid breakfast_Id)
    {
        var breakfastJSON = fireBaseService.Read("Breakfast/", breakfast_Id);
        BreakfastModel breakfastModel = JsonConvert.DeserializeObject<BreakfastModel>(breakfastJSON);

        return breakfastModel;
    }

    public void UpsertBreakfast(Guid breakfast_Id, BreakfastModel breakfastModel)
    {
        
    }

    public void DeleteBreakfast(Guid breakfast_id)
    {
        throw new NotImplementedException();
    }
}



