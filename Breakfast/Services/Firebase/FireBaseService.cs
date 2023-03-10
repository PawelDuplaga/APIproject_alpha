using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Breakfast.ApiLogger;
using Breakfast.Utils;

namespace Breakfast.Services.Firebase;

public class FireBaseService : IFireBaseService
{
    FirebaseServiceConfig _config;
    IFirebaseClient _client;
    ILogger _logger;
    ILoggerProvider _apiLoggerProvider;

    public FireBaseService()
    {
        LoadConfig();
        ConnectToDatabase();
    }
    
    private void LoadConfig()
    {
        _apiLoggerProvider = new ApiLoggerProvider();
        _logger = _apiLoggerProvider.CreateLogger("FirebaseLogger");
        _config = XmlConfigReader<FirebaseServiceConfig>.GetConfig(ApiBreakfastConstants.FIREBASE_CONFIG_FILE_PATH);
    }

    private void ConnectToDatabase()
    {
       
        IFirebaseConfig fconfig = new FirebaseConfig
        {
            AuthSecret= _config.AuthSecret,
            BasePath = _config.BasePath
        };

        _client = new FireSharp.FirebaseClient(fconfig);
        
    }

    public async Task<FirebaseResult> Create(string collection_path, Guid data_Id, dynamic data_obj)
    {
        SetResponse response = await _client.SetAsync(collection_path + data_Id, data_obj);

        if(response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            _logger.LogDebug(String.Format("Object with given id [{0}] was created in database", data_Id));
            return FirebaseResult.Success;
        }

        return FirebaseResult.Error;    
    }

    public async Task<(FirebaseResult,string?)> Read(string collection_path, Guid data_Id)
    {

        try
        {
            FirebaseResponse firebaseResponse = await _client.GetAsync(collection_path + data_Id);

            if(firebaseResponse.StatusCode == System.Net.HttpStatusCode.OK && firebaseResponse.ResultAs<Object>() != null)
            {
                _logger.LogDebug(String.Format("Object with given id [{0}] was found in database", data_Id));
                return (FirebaseResult.Success, firebaseResponse.Body);
            }
            if(firebaseResponse.StatusCode == System.Net.HttpStatusCode.OK && firebaseResponse.ResultAs<Object>() == null)
            {
                _logger.LogDebug(String.Format("Object with given id [{0}] was not found in database", data_Id));
                return (FirebaseResult.NotFound, null);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }

        return (FirebaseResult.Error, null);
    }

    public async Task<FirebaseResult> Update(string collection_path, Guid data_id, dynamic data_obj)
    {
        FirebaseResponse getResponse = await _client.GetAsync(collection_path + data_id);
        if(getResponse.StatusCode == System.Net.HttpStatusCode.OK && getResponse.ResultAs<Object>() == null)
        {
            FirebaseResponse response = await _client.UpdateAsync(collection_path + data_id, data_obj);
            return FirebaseResult.Success;
        }
        if(getResponse.StatusCode == System.Net.HttpStatusCode.OK && getResponse.ResultAs<Object>() != null)
        {
            FirebaseResponse response = await _client.UpdateAsync(collection_path + data_id, data_obj);
            return FirebaseResult.Updated;
        }
        
        return FirebaseResult.Error;

    }

    public async Task<FirebaseResult> Delete(string collection_path, Guid data_id)
    {
        FirebaseResponse getResponse = await _client.GetAsync(collection_path + data_id);
        if(getResponse.StatusCode == System.Net.HttpStatusCode.OK && getResponse.ResultAs<Object>() != null)
        {
            FirebaseResponse response = await _client.DeleteAsync(collection_path + data_id);
            return FirebaseResult.Success;
        }
        if(getResponse.StatusCode == System.Net.HttpStatusCode.OK && getResponse.ResultAs<Object>() == null)
        {
            FirebaseResponse response = await _client.DeleteAsync(collection_path + data_id);
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