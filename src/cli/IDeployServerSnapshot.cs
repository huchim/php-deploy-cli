using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phpdeploy
{
    interface IDeployServerSnapshot
    {
        /// <summary>
        /// Devuelve una lista de cambios entre <see cref="getServerLastTimestamp" /> y la marca de tiempo actual.
        /// </summary>
        /// <param name="compareWithTimestamp">Última marca de tiempo en el servidor.</param>
        /// <returns></returns>
        IPackage getSnapshotDifference(int compareWithTimestamp);

        IPackage getLastChanges(int compareWithTimestamp);

        int uploadSnapshotFiles(IPackage package);

        int createSnapshot();

        int createSnapshotWithTimestamp(int timestamp);
    }
}
