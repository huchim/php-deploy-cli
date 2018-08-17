namespace phpdeploy
{
    using phpdeploy.CliOptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Deployer
    {
        private DeployOptions c;

        public Deployer(DeployOptions c)
        {
            this.c = c;
        }

        public int RunWithExitCode()
        {
            var deployUrl = this.c.GetUrl();
            Console.WriteLine("Dirección del servidor: {0}", deployUrl);

            return 0;
        }
    }
}
