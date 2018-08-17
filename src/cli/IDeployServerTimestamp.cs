using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace phpdeploy
{
    interface IDeployServerTimestamp
    {
        /// <summary>
        /// Establece la marca de tiempo del repositorio.
        /// </summary>
        /// <param name="currentTimestamp"></param>
        void setCurrentTimeStamp(int currentTimestamp);

        /// <summary>
        /// Obtiene la última marca de tiempo registrada en el servidor.
        /// </summary>
        /// <returns></returns>
        int getServerLastTimestamp();


    }
}
