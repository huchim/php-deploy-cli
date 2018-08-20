namespace phpdeploy
{
    using phpdeploy.CliOptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Deployer
    {
        private DeployOptions options;

        public Deployer(DeployOptions deployOptions)
        {
            this.options = deployOptions;
        }

        public int RunWithExitCode()
        {
            var deployUrl = this.options.GetUrl();
            var appName = this.options.DeployApp;
            var stageName = this.options.DeployStage;
            var repositoryFolder = this.options.GetCurrentWorkingDirectory();
            var publishFolder = this.options.RepositoryPublishFolder;
            var deployServer = new DeployServer();

            Console.WriteLine("Dirección del servidor: {0}", deployUrl);
            Console.WriteLine("APP: {0}.{1}", appName, stageName);
            Console.WriteLine("Repository: {0} [/{1}]", repositoryFolder, publishFolder);

            deployServer.setUrl(deployUrl);
            deployServer.setPackageInfo(appName, stageName);

            // Crear una referencia al repositorio.
            var repository = new GitRepository();
            repository.setFolder(repositoryFolder);
            repository.setPublishFolder(publishFolder);

            // Crear un paquete con la lista de archivos.
            Console.WriteLine("Creando paquete de archivos.");
            var package = repository.CreateFileList();

            Console.WriteLine("Recuperando la marca de tiempo del repositorio");
            deployServer.setCurrentTimeStamp(repository.getTimeStamp());

            Console.WriteLine("Generando bloqueo exclusivo para esta marca de tiempo.");
            deployServer.lockUsingCurrentTimestamp();            

            Console.WriteLine("Comparando este directorio con el servidor...");
            var nextPackage = deployServer.compareWith(package);

            package.export(this.options.Output);

            return 0;
        }
    }
}
