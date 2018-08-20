namespace phpdeploy
{
    using Ionic.Zip;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    class Package : IPackage
    {
        IRepository repository = null;
        string[] changes = new string[] { };
        Dictionary<string, IRepositoryFile> files = new Dictionary<string, IRepositoryFile>();

        public void savePackageFile(string filename)
        {
            var entries = string.Join(",", this.files.Select(x => x.Value.toJson(x.Key)));
            var content = string.Format("{{ \"commit\": \"none\", \"files\": [ {0} ] }}", entries);

            // Guardar el contenido en un archivo.
            File.WriteAllText(filename, content);
        }

        /// <summary>
        /// Crea un archivo ZIP con el contenido de los cambios y la lista de archivos nuevos.
        /// </summary>
        /// <param name="path">Ruta de acceso.</param>
        public void export(string path)
        {
            this.normalizeFilesPath();

            // Crear archivo con los cambios.
            var buffer = new System.Text.StringBuilder();
            var changesFileName = ".changes";

            // Agregar en la primera línea la marca de tiempo del repositorio.
            buffer.AppendLine(this.getRepository().getTimeStamp().ToString());

            using (ZipFile zip = new ZipFile())
            {
                foreach (var key in this.files.Keys)
                {
                    var file = this.files[key];
                    buffer.AppendLine(file.ToString());

                    // Get folder of the name.
                    var fileName = this.getFileName(file.Name);
                    var zipFolder = Path.GetDirectoryName(file.Name);

                    var e = zip.AddFile(file.AbsolutePath, zipFolder);


                    // File.Copy(file.AbsolutePath, targetFile);
                }

                File.WriteAllText(changesFileName, buffer.ToString());
                var z = zip.AddFile(changesFileName, "");

                zip.Save(path);
            }

        }

        private void normalizeFilesPath()
        {
            var currentDirectory = this.getRepository().getPublishFolder();
            // Console.WriteLine("Directorio de trabajo: {0}", currentDirectory);

            foreach (var key in this.files.Keys)
            {
                // Ignorar el primer caractare separador.
                var fileName = this.getFileName(this.files[key].Name);
                var path = Path.Combine(currentDirectory, fileName);

                // Console.WriteLine(path);

                this.files[key].AbsolutePath = Path.GetFullPath(path);
            }
        }

        private string getFileName(string name)
        {
            var xname = name.Replace('/', Path.DirectorySeparatorChar).Trim();

            if (xname.Substring(0, 1) == Path.DirectorySeparatorChar.ToString())
            {
                return xname.Substring(1);
            }

            return xname;
        }

        public int getChangesCount()
        {
            return this.changes.Length;
        }
        
        public IRepository getRepository()
        {
            return this.repository;
        }

        public void setChanges(string[] fileChanges)
        {
            this.changes = fileChanges;
        }

        public void setFileList(Dictionary<string, IRepositoryFile> dictionary)
        {
            this.files = dictionary;
        }

        public void setRepository(IRepository folder)
        {
            this.repository = folder;
        }

        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
            Console.WriteLine("Intentado crear directorio temporal: {0}", tempDirectory);

            Directory.CreateDirectory(tempDirectory);

            return Path.GetFullPath(tempDirectory);
        }
    }
}
