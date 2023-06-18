using MongoDBChangetrackerWebjobCore.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBChangetrackerWebjobCoreInfra.Services
{
    public class ProcessDatabaseChange : IProcessDatabaseChange
    {

        public bool PushChange(string changeData)
        {
            return false;
        }

    }
}
