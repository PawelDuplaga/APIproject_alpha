using Newtonsoft.Json;
using Breakfast.Configs;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Breakfast.Utils;

namespace Breakfast.Services.Firebase;

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
        string json = File.ReadAllText(ApiBreakfastConstants.FIREBASE_CONFIG_FILE_PATH);
        UserConfig config = JsonConvert.DeserializeObject<UserConfig>(json);

        fconfig = new FirebaseConfig
        {
            AuthSecret= config.AuthSecret,
            BasePath = config.FirebasePath
        };

        fclient = new FireSharp.FirebaseClient(fconfig);
    }

    public async Task<FirebaseResult> Create(string collection_path, Guid data_Id, dynamic data_obj)
    {
        SetResponse response = await fclient.SetAsync(collection_path + data_Id, data_obj);

        if(response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return FirebaseResult.Success;
        }

        return FirebaseResult.Error;    
    }

    public async Task<(FirebaseResult,string?)> Read(string collection_path, Guid data_Id)
    {

        try
        {
            FirebaseResponse firebaseResponse = await fclient.GetAsync(collection_path + data_Id);

            if(firebaseResponse.StatusCode == System.Net.HttpStatusCode.OK && firebaseResponse.ResultAs<Object>() != null)
            {
                return (FirebaseResult.Success, firebaseResponse.Body);
            }
            if(firebaseResponse.StatusCode == System.Net.HttpStatusCode.OK && firebaseResponse.ResultAs<Object>() == null)
            {
                return (FirebaseResult.NotFound, null);
            }
        }
        catch
        {
            
        }

        return (FirebaseResult.Error, null);
    }

    public async Task<FirebaseResult> Update(string collection_path, Guid data_id, dynamic data_obj)
    {
        FirebaseResponse getResponse = await fclient.GetAsync(collection_path + data_id);
        if(getResponse.StatusCode == System.Net.HttpStatusCode.OK && getResponse.ResultAs<Object>() == null)
        {
            FirebaseResponse response = await fclient.UpdateAsync(collection_path + data_id, data_obj);
            return FirebaseResult.Success;
        }
        if(getResponse.StatusCode == System.Net.HttpStatusCode.OK && getResponse.ResultAs<Object>() != null)
        {
            FirebaseResponse response = await fclient.UpdateAsync(collection_path + data_id, data_obj);
            return FirebaseResult.Updated;
        }
        
        return FirebaseResult.Error;

    }

    public async Task<FirebaseResult> Delete(string collection_path, dynamic data_id)
    {
        FirebaseResponse getResponse = await fclient.GetAsync(collection_path + data_id);
        if(getResponse.StatusCode == System.Net.HttpStatusCode.OK && getResponse.ResultAs<Object>() != null)
        {
            FirebaseResponse response = await fclient.DeleteAsync(collection_path + data_id);
            return FirebaseResult.Success;
        }
        if(getResponse.StatusCode == System.Net.HttpStatusCode.OK && getResponse.ResultAs<Object>() == null)
        {
            FirebaseResponse response = await fclient.DeleteAsync(collection_path + data_id);
            return FirebaseResult.NotFound;
        }

        return FirebaseResult.Error;
    }

}



/* FirebaseResponse is a class in the Firebase C# library that represents the response from a Firebase Realtime Database operation. 
It contains information such as the status of the operation (success or failure), the HTTP status code, and the JSON data returned from the server.
SetResponse is a subclass of FirebaseResponse that is specific to the "set" operation, which is used to write data to the database. 
It contains additional information such as the unique key of the newly-written data and the priority of the data.
In general, you would use FirebaseResponse when you want to handle the response from any type of Firebase Realtime Database operation, 
and SetResponse when you specifically want to handle the response from a "set" operation. */

// Later we could implement handling of all type of Errors, for now its enough