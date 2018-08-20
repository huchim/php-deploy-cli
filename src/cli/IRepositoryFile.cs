namespace phpdeploy
{
    interface IRepositoryFile
    {
        string Name { get; set; }

        string AbsolutePath { get; set; }

        RepositoryFileAction Action { get; set; }

        void fromString(string fileInfo);

        string toJson(string keyName);
    }
}
