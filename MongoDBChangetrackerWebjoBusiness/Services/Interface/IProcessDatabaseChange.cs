using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBChangetrackerWebjobCore.Services.Interface
{
    public interface IProcessDatabaseChange
    {
        bool PushChange(string changeData);
    }
}
