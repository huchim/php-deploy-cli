namespace phpdeploy
{
    using phpdeploy.http;
    using System;
    using System.IO;
    using System.Net;

    class DeployServerBase : IWithHttpRequest
    {
        string appName = "";
        string stageName = "";
        string url = "";
        protected int currentTimestamp = 0;        
        NetworkCredential credentials = null;

        public void setPackageInfo(string appName, string stageName)
        {
            this.appName = appName;
            this.stageName = stageName;
        }

        public string getUrl()
        {
            return this.url;
        }

        protected static int UploadFile(string url, string fileName, NetworkCredential credentials = null)
        {
            if (!File.Exists(fileName))
            {
                // El archivo no se creó.
                return -1;
            }

            Console.WriteLine("Subiendo con URL: {0}", url);
            var response = HttpRequest.UPLOAD(url, fileName, credentials);

            return 0;
        }

        protected static string[] DownloadStringArrayWithPost(string url, string filename, NetworkCredential credentials = null)
        {
            var response = HttpRequest.UPLOAD(url, filename, credentials);

            if (!string.IsNullOrEmpty(response))
            {
                return response.Split('\n');
            }

            return new string[] { };
        }

        protected static string[] DownloadStringArray(string url, NetworkCredential credentials = null)
        {
            Console.WriteLine("Solicitud a la URL: {0}", url);
            var response = HttpRequest.GET(url, credentials);

            if (!string.IsNullOrEmpty(response))
            {
                return response.Split('\n');
            }

            return new string[] { };
        }

        protected static string DownloadString(string url, NetworkCredential credentials = null)
        {
            Console.WriteLine("Solicitud a la URL: {0}", url);
            return HttpRequest.GET(url, credentials);
        }

        protected string getAppName()
        {
            return this.appName;
        }

        protected string getStageName()
        {
            return this.stageName;
        }

        public NetworkCredential getCredentials()
        {
            return this.credentials;
        }

        public void setCredentials(string username, string password)
        {
            this.credentials = new NetworkCredential(username, password);
        }

        public void setUrl(string deployServerUrl)
        {
            this.url = deployServerUrl;
        }

        protected string getLockServerUrl(int lockTimestamp)
        {
            return string.Format("{0}/lock/{1}", this.getBaseUrl(), lockTimestamp);
        }

        protected string getUnlockServerUrl(int lockTimestamp)
        {
            return string.Format("{0}/unlock/{1}", this.getBaseUrl(), lockTimestamp);
        }

        protected string getCreateSnapshotUrl(int lockTimestamp, int compareWithTimestamp)
        {
            return string.Format("{0}/snapshot/{1}/{2}", this.getBaseUrl(), lockTimestamp, compareWithTimestamp);
        }

        protected string getSnapshotDifferenceUrl(int fromTimestamp, int toTimestamp)
        {
            return string.Format("{0}/diff/{1}/{2}", this.getBaseUrl(), fromTimestamp, toTimestamp);
        }

        protected string getLastSnapshotDifferenceUrl(int fromTimestamp)
        {
            return string.Format("{0}/getlastdiff/{1}", this.getBaseUrl(), fromTimestamp);
        }

        protected string getUploadSnapshotFilesUrl(int timestamp)
        {
            return string.Format("{0}/upload/{1}", this.getBaseUrl(), timestamp);
        }

        protected string getSnapshotDifferenceUrlWithLocal(int lockTimestamp)
        {
            return string.Format("{0}/diff/{1}", this.getBaseUrl(), lockTimestamp);
        }

        protected string getServerTimestampsUrl()
        {
            return string.Format("{0}/timestamps", this.getBaseUrl());
        }

        protected string getBaseUrl()
        {
            return string.Format("{0}{1}/{2}", this.url, this.getAppName(), this.getStageName());
        }
    }
}
