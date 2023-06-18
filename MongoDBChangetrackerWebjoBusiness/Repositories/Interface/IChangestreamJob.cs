using System.Text.Json;

namespace MongoDBChangetrackerWebjobCore.Repositories.Interface
{
    public interface IChangestreamJob
    {
        void WatchDatabsechange(Func<string, bool> operation);
    }
}
