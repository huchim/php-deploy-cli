using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace phpdeploy
{
    interface IWithHttpRequest
    {
        void setUrl(string deployServerUrl);

        string getUrl();

        void setCredentials(string username, string password);

        NetworkCredential getCredentials();
    }
}
