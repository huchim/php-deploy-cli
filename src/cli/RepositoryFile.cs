using System.Linq;

namespace phpdeploy
{
    class RepositoryFile : IRepositoryFile
    {
        public string Name { get; set; }

        public string AbsolutePath { get; set; }

        public RepositoryFileAction Action { get; set; } = RepositoryFileAction.NONE;

        public string toJson(string keyName)
        {
            return string.Format("{{\"CheckSum\": \"{0}\", \"Name\": \"{1}\", \"Action\": \"{2}\"}}", keyName, Name.Replace("\\", "\\\\"), Action);
        }

        public override string ToString()
        {
            var preffix = "?";

            if (Action == RepositoryFileAction.CREATE)
            {
                preffix = "+";
            }

            if (Action == RepositoryFileAction.DELETE)
            {
                preffix = "-";
            }

            if (Action == RepositoryFileAction.UPDATE)
            {
                preffix = "*";
            }

            return string.Format("{0} {1} {2}", preffix, Name, Action);
        }

        public void fromString(string fileInfo)
        {
            if (string.IsNullOrEmpty(fileInfo.Trim()))
            {
                return;
            }

            fileInfo = fileInfo.Trim();
            var containsAction = (new char[] { '+', '*', '-' }).Contains(fileInfo[0]);

            if (!containsAction)
            {
                fileInfo = " " + fileInfo;
            }

            var parts = fileInfo.Split(' ');

            switch (parts[0])
            {
                case "+": this.Action = RepositoryFileAction.CREATE; break;
                case "-": this.Action = RepositoryFileAction.DELETE; break;
                case "*": this.Action = RepositoryFileAction.UPDATE; break;
                default: this.Action = RepositoryFileAction.NONE; break;
            }

            this.Name = parts.Length > 1 ? parts[1] : "";
        }
    }
}
