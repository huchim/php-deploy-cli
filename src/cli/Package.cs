using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace phpdeploy
{
    class Package : IPackage
    {
        IRepository repository = null;
        string[] changes = new string[] { };

        /// <summary>
        /// Crea un archivo ZIP con el contenido de los cambios y la lista de archivos nuevos.
        /// </summary>
        /// <param name="path">Ruta de acceso.</param>
        public void export(string path)
        {
            File.WriteAllText(path, "dummy file");
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

        public void setRepository(IRepository folder)
        {
            this.repository = folder;
        }
    }
}
