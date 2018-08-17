using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace phpdeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new GitRepository();
            var urlDeployServer = "http://localhost:8000";
            var appName = "sao";
            var stageName = "development";

            m.setFolder(@"H:\git_projects3\avance-obra\api");
            Console.WriteLine("Directorio GIT: {0}", m.getFolder());            
            Console.WriteLine("Recuperando marca de tiempo de los cambios actuales.");
            var changesTimestamp = m.getTimeStamp();
            Console.WriteLine("Marca de tiempo: {0}", changesTimestamp);


            foreach (var item in m.getFileList())
            {
                Console.WriteLine("{0} = {1}", item.Key, item.Value.Name);
            }

            Console.ReadKey(false);
            return;

            var deployServerInstance = new DeployServer();

            deployServerInstance.setUrl(urlDeployServer);
            // deployServerInstance.setCredentials("huchim", "152560");
            deployServerInstance.setPackageInfo(appName, stageName);
            deployServerInstance.setCurrentTimeStamp(changesTimestamp);

            Console.WriteLine("Recuperando el último timestamp en el servidor.");
            int lastServerTimestamp = deployServerInstance.getServerLastTimestamp();
            Console.WriteLine("El último cambio es: {0}", lastServerTimestamp);

            if (changesTimestamp == lastServerTimestamp)
            {
                Console.WriteLine("El servidor tiene la misma marca de tiempo que este reposiorio.");
                Console.ReadKey(false);
                return;
            }

            Console.WriteLine("Bloqueando recurso para poder trabajar");
            deployServerInstance.lockUsingCurrentTimestamp();

            Console.WriteLine("Creando lista de cambios con este timestamp");
            deployServerInstance.createSnapshot();


            

            Console.ReadKey(false);
            return;



            Console.WriteLine("Recuperando lista de cambios.");
            var x = deployServerInstance.getLastChanges(lastServerTimestamp);
            x.setRepository(m);

            Console.WriteLine("Total de cambios: {0}", x.getChangesCount());


            if (x.getChangesCount() > 0)
            {
                Console.WriteLine("Aplicando cambios en el servidor.");
                deployServerInstance.uploadSnapshotFiles(x);
            }

            Console.WriteLine("Desbloqueando recurso para poder trabajar");
            deployServerInstance.unlockUsingCurrentTimestamp();

            //
            Console.WriteLine("Fin del proceso");
            Console.ReadKey(false);
            
        }
    }
}
