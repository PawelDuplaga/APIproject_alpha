namespace Breakfast.Services.Breakfast;

public interface IDatabaseWrapper
{
    
    public void ConnectToDatabase();

    public void Create(string db_path, Object data_obj, Guid data_Id);
    public void GetAllFromCollection(string collection_name);

    public void GetOneFromCollection(string collection_name, string Id);

    public void SaveAllToCollection();




}