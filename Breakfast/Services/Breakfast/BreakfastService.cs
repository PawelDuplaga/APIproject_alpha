using Breakfast.Models;
using Newtonsoft.Json;
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

    public void CreateBreakfast(Guid breakfast_Id, BreakfastModel breakfastModel)
    {
        _fireBaseService.Create(FIREBASE_BREAKFAST_COLLECTION_PATH, breakfast_Id, breakfastModel);
    }

    public ErrorOr<BreakfastModel> GetBreakfast(Guid breakfast_Id)
    {
        var breakfastJSON = _fireBaseService.Read(FIREBASE_BREAKFAST_COLLECTION_PATH, breakfast_Id);
        Console.WriteLine(breakfastJSON);
        BreakfastModel breakfastModel = JsonConvert.DeserializeObject<BreakfastModel>(breakfastJSON);

        return breakfastModel;
    }

    public void UpsertBreakfast(Guid breakfast_Id, BreakfastModel breakfastModel)
    {
        _fireBaseService.Update(FIREBASE_BREAKFAST_COLLECTION_PATH, breakfast_Id, breakfastModel);
    }

    public void DeleteBreakfast(Guid breakfast_id)
    {
        _fireBaseService.Delete(FIREBASE_BREAKFAST_COLLECTION_PATH, breakfast_id);
    }
}



