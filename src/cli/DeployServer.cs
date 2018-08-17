using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace phpdeploy
{
    class DeployServer : DeployServerBase, IDeployServer, IDeployServerTimestamp
    {
        int lockTimestamp = 0;

        public int createSnapshot()
        {
            return this.createSnapshotWithTimestamp(this.currentTimestamp);
        }

        public int createSnapshotWithTimestamp(int timestamp)
        {
            var url = this.getCreateSnapshotUrl(this.currentTimestamp, timestamp);
            var results = DownloadString(url, this.getCredentials());

            return 0;
        }

        public int getServerLastTimestamp()
        {
            var url = this.getServerTimestampsUrl();
            var timestamps = DownloadStringArray(url, this.getCredentials());
            var response = timestamps.DefaultIfEmpty("0").First();
            int output;

            if (!int.TryParse(response, out output))
            {
                return -1;
            }

            return output;
        }

        public IPackage getSnapshotDifference(int compareWithTimestamp)
        {
            var package = new Package();
            var url = this.getSnapshotDifferenceUrl(this.currentTimestamp, compareWithTimestamp);
            var fileChanges = DownloadStringArray(url, this.getCredentials());

            package.setChanges(fileChanges);

            return package;
        }

        public IPackage getLastChanges(int compareWithTimestamp)
        {
            var package = new Package();
            var url = this.getSnapshotDifferenceUrl(this.currentTimestamp, 0);
            var fileChanges = DownloadStringArray(url, this.getCredentials());

            package.setChanges(fileChanges);

            return package;
        }


        public void setCurrentTimeStamp(int currentTimestamp)
        {
            this.currentTimestamp = currentTimestamp;
        }

        public void lockUsingCurrentTimestamp()
        {
            this.lockTimestamp = this.currentTimestamp;
            var url = this.getLockServerUrl(this.lockTimestamp);
            var results = DownloadString(url, this.getCredentials());
        }

        public void unlockUsingCurrentTimestamp()
        {
            var url = this.getUnlockServerUrl(this.lockTimestamp);
            var results = DownloadString(url, this.getCredentials());
        }

        public int uploadSnapshotFiles(IPackage package)
        {
            // Acceder a un archivo temporal para generar el ZIP.
            var fileName = Path.GetTempPath() + Guid.NewGuid().ToString();
            var url = this.getUploadSnapshotFilesUrl(this.currentTimestamp);

            // Generar el archivo.
            package.export(fileName);

            return UploadFile(url, fileName, this.getCredentials());
        }

        private int getLocktimestamp()
        {
            return this.lockTimestamp;
        }
    }
}
