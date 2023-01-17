namespace Breakfast.Services.Firebase;

public interface IFireBaseService
{
    
    public Task<FirebaseResult> Create(string collection_path, Guid data_id, dynamic data_obj);

    public Task<(FirebaseResult,string?)> Read(string collection_path, Guid data_id);

    public Task<FirebaseResult> Update(string collection_path, Guid data_id, dynamic data_obj);

    public Task<FirebaseResult> Delete(string collection_path, Guid data_id);


}