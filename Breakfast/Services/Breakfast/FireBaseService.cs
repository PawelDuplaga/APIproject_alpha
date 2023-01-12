using Newtonsoft.Json;
using Breakfast.Configs;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Breakfast.Models;


namespace Breakfast.Services.Breakfast;

public class FireBaseService : IFireBaseService
{

    IFirebaseConfig fconfig;
    IFirebaseClient fclient;


    public FireBaseService()
    {
        ConnectToDatabase();
    }
    


    private void ConnectToDatabase()
    {
        string json = File.ReadAllText(@"Configs/UserConfig.json");
        UserConfig config = JsonConvert.DeserializeObject<UserConfig>(json);

        fconfig = new FirebaseConfig
        {
            AuthSecret= config.AuthSecret,
            BasePath = config.FirebasePath
        };

        fclient = new FireSharp.FirebaseClient(fconfig);
        Console.WriteLine(config.AuthSecret);
        Console.WriteLine(config.FirebasePath);

    }

    public async Task<Result> Create(string collection_path, Guid data_Id, Object data_obj)
    {
     
        SetResponse response = await fclient.SetAsync(collection_path + data_Id, data_obj);

        if(response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Result.Success;
        }
        else
        {
            return Result.Error;
        }
    }

    public async Task<(Result,string)> Read(string collection_path, Guid data_Id)
    {
        FirebaseResponse firebaseResponse = await fclient.GetAsync(collection_path + data_Id);
        if(firebaseResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return (Result.NotFound,"404 Not Found");
        } 
        else
        {
            return (Result.Success, firebaseResponse.Body);
        }
    }

    public async Task<Result> Update(string collection_path, Guid data_id, object data_obj)
    {
        SetResponse response = await fclient.SetAsync(collection_path + data_id, data_obj);

        if(response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Result.Success;
        }
        else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return Result.NotFound;
        }
        else
        {
            return Result.Error;
        }
    }

    public async Task<Result> Delete(string collection_path, Guid data_id)
    {
        FirebaseResponse response = await fclient.DeleteAsync(collection_path + data_id);

        if(response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Result.Success;
        }
        else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return Result.NotFound;
        }
        else
        {
            return Result.Error;
        }
    }


    
}



/* FirebaseResponse is a class in the Firebase C# library that represents the response from a Firebase Realtime Database operation. 
It contains information such as the status of the operation (success or failure), the HTTP status code, and the JSON data returned from the server.
SetResponse is a subclass of FirebaseResponse that is specific to the "set" operation, which is used to write data to the database. 
It contains additional information such as the unique key of the newly-written data and the priority of the data.
In general, you would use FirebaseResponse when you want to handle the response from any type of Firebase Realtime Database operation, 
and SetResponse when you specifically want to handle the response from a "set" operation. */

// Later we could implement handling of all type of Errors, for now its enough