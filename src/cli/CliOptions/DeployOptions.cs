using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phpdeploy.CliOptions
{
    class DeployOptions
    {
        private Uri uri;

        [Option("url", HelpText = "Direccion URL del servidor.")]
        public string ServerUrl { get; set; }

        [Option("host", HelpText = "Direción o IP del servidor remoto.")]
        public string ServerHost { get; set; }

        [Option("port", Default = 80, HelpText = "Direción o IP del servidor remoto.")]
        public int ServerPort { get; set; }

        [Option('c', "controller", Default = "", HelpText = "Sub carpeta de la aplicación.")]
        public string ServerController { get; set; }

        [Option("protocol", Default = "https", HelpText = "Direción o IP del servidor remoto.")]
        public string ServerProtocol { get; set; }

        [Option('r', "repository", Default = "", HelpText = "Carpeta de repositorio.")]
        public string RepositoryFolder { get; set; }

        [Option("publish", Default = ".publish", HelpText = "Carpeta a publicar..")]
        public string RepositoryPublishFolder { get; set; }

        [Option('u', "username", Default = "", HelpText = "Carpeta a publicar..")]
        public string ServerUser { get; set; }

        [Option('p', "password", Default = "", HelpText = "Carpeta a publicar..")]
        public string ServerPassword { get; set; }

        [Option('a', "app", Default = "", HelpText = "Carpeta a publicar..")]
        public string DeployApp { get; set; }

        [Option('s', "stage", Default = "", HelpText = "Carpeta a publicar..")]
        public string DeployStage { get; set; }

        public void ParseUrl()
        {
            if (string.IsNullOrEmpty(this.ServerUrl))
            {
                this.ServerUrl = string.Format(
                    "{0}://{1}:{2}{3}",
                    string.IsNullOrEmpty(this.ServerProtocol) ? "https" : this.ServerProtocol,
                    string.IsNullOrEmpty(this.ServerHost) ? "localhost" : this.ServerHost,
                    this.ServerPort == 0 ? 80 : this.ServerPort,
                    string.IsNullOrEmpty(this.ServerController) ? "" : "/" + this.ServerController
                );
            }

            this.uri = new Uri(this.ServerUrl);
        }

        public string GetUrl()
        {
            this.ParseUrl();

            return this.uri.ToString();
        }
    }
}
