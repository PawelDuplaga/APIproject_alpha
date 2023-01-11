using Breakfast.Models;
using Google.Cloud.Firestore;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;


namespace Breakfast.Services.Breakfast;

public class BreakfastService : IBreakfastService
{
    IFirebaseConfig config;
    IFirebaseClient client;
     private static readonly Dictionary<Guid,BreakfastModel> _breakfasts = new Dictionary<Guid, BreakfastModel>();

    public BreakfastService()
    {
        config = new FirebaseConfig
        {
            AuthSecret= "ojS09wi2FKqqj8QpmTgSvYrd0L6lfmDU2QUHtXKM", 
            BasePath = "https://breakfastapi-6e85a-default-rtdb.europe-west1.firebasedatabase.app"
        };
    }

    public void CreateBreakfast(BreakfastModel breakfastModel)
    {
        _breakfasts.Add(breakfastModel.Id,breakfastModel);

        try
        {
            client = new FireSharp.FirebaseClient(config);
            var data = breakfastModel;
            //PushResponse response = client.Push("Breakfasts/",data);
            SetResponse setResponse = client.Set("Breakfasts/" + data.Id, data);

            if(setResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("ERROR");
            }

        }
        catch (Exception ex)
        {

                Console.WriteLine(ex.Message);
        }

    }


    public BreakfastModel GetBreakfast(Guid id)
    {
        return _breakfasts[id];
    }
}


