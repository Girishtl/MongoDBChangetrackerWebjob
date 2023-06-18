using MongoDBChangetrackerWebjobCore.Services.Interface;

namespace MongoDBChangetrackerWebjobCoreInfra.Services
{
    public class ProcessDatabaseChange : IProcessDatabaseChange
    {

        public bool PushChange(string changeData)
        {
            return true;
        }

    }
}
