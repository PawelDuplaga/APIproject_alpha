using Breakfast.Models;
using Newtonsoft.Json;
using Breakfast.ServiceErrors;
using ErrorOr;

namespace Breakfast.Services.Breakfast;

public class BreakfastService : IBreakfastService
{
    private IFireBaseService _fireBaseService;
    private static readonly string FIREBASE_BREAKFAST_COLLECTION_PATH = "Breakfast/";

    public BreakfastService()
    {
        _fireBaseService = new FireBaseService();
    }

    public async Task CreateBreakfast(Guid breakfast_Id, BreakfastModel breakfastModel)
    {
        var result = await _fireBaseService.Create(FIREBASE_BREAKFAST_COLLECTION_PATH,
                                                    breakfast_Id,
                                                    breakfastModel);

        if(result == Result.Success) return;
        else return;
        
    }

    public async Task<ErrorOr<BreakfastModel>> GetBreakfast(Guid breakfast_Id)
    {
        var (result, breakfastJSON) = await _fireBaseService.Read(FIREBASE_BREAKFAST_COLLECTION_PATH, breakfast_Id);

        if(result == Result.Success)
        {
            BreakfastModel breakfastModel = JsonConvert.DeserializeObject<BreakfastModel>(breakfastJSON);
            return breakfastModel;
        }
        if(result == Result.NotFound)
        {
            return Errors.Breakfast.NotFound;
        }
        else
        {
            return Errors.Breakfast.Unexpected;
        }
    }

    public async Task UpsertBreakfast(Guid breakfast_Id, BreakfastModel breakfastModel)
    {
        var result = await _fireBaseService.Update(FIREBASE_BREAKFAST_COLLECTION_PATH,
                                                    breakfast_Id, 
                                                    breakfastModel);
    }

    public async Task DeleteBreakfast(Guid breakfast_id)
    {
        var result = await _fireBaseService.Delete(FIREBASE_BREAKFAST_COLLECTION_PATH, breakfast_id);
    }
}



