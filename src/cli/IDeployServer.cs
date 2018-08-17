using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phpdeploy
{
    interface IDeployServer: IDeployServerLock, IDeployServerSnapshot, IDeployServerTimestamp, IWithHttpRequest
    {
        void setPackageInfo(string appName, string stageName);        
    }
}
