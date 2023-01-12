using Breakfast.Models;
using Newtonsoft.Json;
using Breakfast.ServiceErrors;
using Breakfast.Services.Firebase;
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

    public async Task<ErrorOr<Created>> CreateBreakfast(Guid breakfast_Id, BreakfastModel breakfastModel)
    {
        var FirebaseResult = await _fireBaseService.Create(FIREBASE_BREAKFAST_COLLECTION_PATH,
                                                    breakfast_Id,
                                                    breakfastModel);

        return ErrorOr.Result.Created;
        
    }

    public async Task<ErrorOr<BreakfastModel>> GetBreakfast(Guid breakfast_Id)
    {
        var (FirebaseResult, breakfastJSON) = await _fireBaseService.Read(FIREBASE_BREAKFAST_COLLECTION_PATH, breakfast_Id);

        if(FirebaseResult == FirebaseResult.Success)
        {
            BreakfastModel breakfastModel = JsonConvert.DeserializeObject<BreakfastModel>(breakfastJSON);
            return breakfastModel;
        }
        if(FirebaseResult == FirebaseResult.NotFound)
        {
            return Errors.Breakfast.NotFound;
        }
        else
        {
            return Errors.Breakfast.Unexpected;
        }
    }

    public async Task<ErrorOr<UpsertedBreakfast>> UpsertBreakfast(Guid breakfast_Id, BreakfastModel breakfastModel)
    {
        //TODO: when upserting new Id => create new Breakfast ?? 
        FirebaseResult FirebaseResult = await _fireBaseService.Update(FIREBASE_BREAKFAST_COLLECTION_PATH,
                                                    breakfast_Id, 
                                                    breakfastModel);
        bool isNewlyCreated(FirebaseResult FirebaseResult) => FirebaseResult == FirebaseResult.Success ? true : false;

        return new UpsertedBreakfast(isNewlyCreated(FirebaseResult));
        
    }

    public async Task<ErrorOr<Deleted>> DeleteBreakfast(Guid breakfast_id)
    {
        FirebaseResult FirebaseResult = await _fireBaseService.Delete(FIREBASE_BREAKFAST_COLLECTION_PATH, breakfast_id);

        if(FirebaseResult == FirebaseResult.Success){
            return ErrorOr.Result.Deleted;
        }
        if(FirebaseResult == FirebaseResult.NotFound){
            return Errors.Breakfast.NotFound;
        }
        else return Errors.Breakfast.Unexpected;
    }
}



