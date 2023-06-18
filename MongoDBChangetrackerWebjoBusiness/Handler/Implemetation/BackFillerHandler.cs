using MongoDBChangetrackerWebjobCore.Repositories.Interface;
using MongoDBChangetrackerWebjobCore.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBChangetrackerWebjobCore.Handler.Implemetation
{
    public class BackFillerHandler
    {

        private readonly IChangestreamJob _changestreamJob;

        private readonly IProcessDatabaseChange _processDatabaseChange;


        public BackFillerHandler(IChangestreamJob changestreamJob, IProcessDatabaseChange processDatabaseChange)
        {
            _changestreamJob = changestreamJob;
            _processDatabaseChange = processDatabaseChange;
        }

        void StartBackFiller()
        {
            _changestreamJob.WatchDatabsechange(_processDatabaseChange.PushChange);


        }
    }
}
