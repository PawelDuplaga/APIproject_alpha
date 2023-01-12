using Newtonsoft.Json;
using Breakfast.Configs;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;


namespace Breakfast.Services.Breakfast;

public class FireBaseService : IDatabaseWrapper
{

    IFirebaseConfig fconfig;
    IFirebaseClient fclient;


    public FireBaseService()
    {
        ConnectToDatabase();
    }
    


    public void ConnectToDatabase()
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

    public void Create(string db_path, Object data_obj, Guid data_Id)
    {
        try
        {
            SetResponse setResponse = fclient.Set("Breakfasts/" + data_Id, data_obj);

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


    public void GetAllFromCollection(string collection_name)
    {
        throw new NotImplementedException();
    }

    public void GetOneFromCollection(string collection_name, string Id)
    {
        throw new NotImplementedException();
    }

    public void SaveAllToCollection()
    {
        throw new NotImplementedException();
    }
}