namespace phpdeploy
{
    using CommandLine;
    using phpdeploy.CliOptions;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<DeployOptions>(args)
                   .WithParsed(o =>
                   {
                       var c = new Deployer(o);
                       var m = c.RunWithExitCode();
                   }).
                   WithNotParsed((errors) =>  {
                       foreach (var error in errors)
                       {
                           Console.WriteLine(error.ToString());
                       }
                   });

        }
    }
}
