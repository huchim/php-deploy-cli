namespace phpdeploy.http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    class HttpRequest
    {
        public static string GET(string url, NetworkCredential credentials)
        {
            var uri = new Uri(url);

            using (var client = new WebClient())
            {
                if (credentials != null)
                {
                    client.Credentials = credentials;
                }

                Console.WriteLine("Host {0}", uri.Host);
                Console.WriteLine("GET {0}", uri.PathAndQuery);

                return client.DownloadString(url);
            }
        }

        public static string UPLOAD(string url, string filename, NetworkCredential credentials)
        {
            var uri = new Uri(url);

            using (var client = new WebClient())
            {
                if (credentials != null)
                {
                    client.Credentials = credentials;
                }

                Console.WriteLine("Host {0}", uri.Host);
                Console.WriteLine("POST {0}", uri.PathAndQuery);

                var response = Encoding.UTF8.GetString(client.UploadFile(uri, "POST", filename));

                return response;
            }
        }
    }
}
