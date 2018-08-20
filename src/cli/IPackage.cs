using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phpdeploy
{
    interface IPackage
    {
        void setRepository(IRepository folder);

        IRepository getRepository();

        void setChanges(string[] fileChanges);

        void savePackageFile(string filename);


        void setFileList(Dictionary<string, IRepositoryFile> dictionary);

        /// <summary>
        /// Exporta los cambios a un archivo comprimido.
        /// </summary>
        /// <remarks>
        /// El archivo contendrá un archivo con la lista de cambios.
        /// </remarks>
        /// <param name="path">
        /// Nombre de archivo.
        /// </param>
        void export(string path);

        int getChangesCount();
    }
}
