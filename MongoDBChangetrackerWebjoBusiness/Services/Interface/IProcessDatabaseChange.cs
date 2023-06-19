namespace MongoDBChangetrackerWebjobCore.Services.Interface
{
    public interface IProcessDatabaseChange
    {
        bool PushChange(string changeData);
    }
}
