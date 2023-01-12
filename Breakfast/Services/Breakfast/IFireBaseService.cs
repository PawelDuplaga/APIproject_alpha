namespace Breakfast.Services.Breakfast;

public interface IFireBaseService
{
    
    public void Create(string collection_path, Guid data_id, Object data_obj);

    public string Read(string collection_path, Guid data_id);

    public void Update(string collection_path, Guid data_id, Object data_obj);

    public void Delete(string collection_path, Guid data_id);


}