namespace Breakfast.Services.Firebase;

public interface IFireBaseService
{
    
    public Task<Result> Create(string collection_path, Guid data_id, Object data_obj);

    public Task<(Result,string?)> Read(string collection_path, Guid data_id);

    public Task<Result> Update(string collection_path, Guid data_id, Object data_obj);

    public Task<Result> Delete(string collection_path, Guid data_id);


}