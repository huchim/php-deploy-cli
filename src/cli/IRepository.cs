using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phpdeploy
{
    interface IRepository
    {
        int getTimeStamp();

        void setFolder(string folder);

        string getFolder();

        void setPublishFolder(string publishFolder);

        string getPublishFolder();

        Dictionary<string, RepositoryFile> getFileList();
    }
}
