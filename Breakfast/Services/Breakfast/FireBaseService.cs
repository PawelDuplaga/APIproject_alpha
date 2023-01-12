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

    public void Create(string collection_path, Guid data_Id, Object data_obj)
    {
        try
        {
            SetResponse setResponse = fclient.Set(collection_path + data_Id, data_obj);

            Console.WriteLine(setResponse.Body);
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

    public async Task<(Result,string)> Read(string collection_path, Guid data_Id)
    {
        FirebaseResponse firebaseResponse = await fclient.GetAsync(collection_path + data_Id);
        if(firebaseResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return (Result.NotFound,"404 Not Found");
        } 
        else
        {
            return (Result.Succes, firebaseResponse.Body);
        }
    }

    public void Update(string collection_path, Guid data_id, object data_obj)
    {
        SetResponse response = fclient.Set(collection_path + data_id, data_obj);
    }

    public void Delete(string collection_path, Guid data_id)
    {
        FirebaseResponse response = fclient.Delete(collection_path + data_id);
    }


    
}



/* FirebaseResponse is a class in the Firebase C# library that represents the response from a Firebase Realtime Database operation. 
It contains information such as the status of the operation (success or failure), the HTTP status code, and the JSON data returned from the server.
SetResponse is a subclass of FirebaseResponse that is specific to the "set" operation, which is used to write data to the database. 
It contains additional information such as the unique key of the newly-written data and the priority of the data.
In general, you would use FirebaseResponse when you want to handle the response from any type of Firebase Realtime Database operation, 
and SetResponse when you specifically want to handle the response from a "set" operation. */