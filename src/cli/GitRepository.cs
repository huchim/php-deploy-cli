using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace phpdeploy
{
    class GitRepository : IRepository
    {
        private string folder = "";
        private string publishFolder = ".publish";

        public string getFolder()
        {
            return this.folder;
        }

        public string getPublishFolder()
        {
            return Path.Combine(this.getFolder(), this.publishFolder);
        }

        public void setPublishFolder(string publishFolder)
        {
            this.publishFolder = publishFolder;
        }


        public int getTimeStamp()
        {
            // Get output from GIT directory
            string arguments = "show -s --format=%at";
            string output = this.runGit(arguments);
            int timestamp;

            if (!int.TryParse(output, out timestamp))
            {
                throw new Exception("El resultado de la operación " + arguments + ", devolvió un valor desconocido: " + output);
            }

            return timestamp;
        }

        public void setFolder(string folder)
        {
            this.folder = folder;
        }

        public IPackage CreateFileList()
        {
            var package = new Package();
            var dictionary = this.getFileList();

            package.setRepository(this);
            package.setFileList(dictionary);

            return package;
        }

        public IPackage LoadFileList(IRepositoryFile[] files)
        {
            var package = new Package();
            var dictionary = new Dictionary<string, IRepositoryFile>();

            foreach (var file in files) {
                dictionary.Add(file.Name, file);
            }

            package.setRepository(this);
            package.setFileList(dictionary);

            return package;
        }

        public Dictionary<string, IRepositoryFile> getFileList()
        {
            var directory = this.getPublishFolder();
            var files = GetFileList(directory);
            var dictionary = new Dictionary<string, IRepositoryFile>();

            foreach (string file in files)
            {
                var fileHash = MD5HashFile(file);
                var fileKey = CreateRepositoryFileKey(file, fileHash, directory);

                if (dictionary.ContainsKey(fileKey))
                {
                    Console.WriteLine("Warning: La clave {0} ya ha sido agregada a la colección {1}", fileHash, file.Replace(directory, ""));
                    continue;
                }

                dictionary.Add(fileKey, new RepositoryFile
                {
                    Name = file.Replace(directory, ""),
                });
            }

            return dictionary;
        }

        private static string[] GetFileList(string directory)
        {
            List<string> files = new List<string>();

            foreach (string file in Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories))
            {
                files.Add(file);
            }

            return files.ToArray();
        }

        private static string CreateRepositoryFileKey(string filename, string hashContent, string workingDirectory)
        {
            byte[] fileNameBytes = Encoding.UTF8.GetBytes(filename.Replace(workingDirectory, ""));
            byte[] hash = MD5.Create().ComputeHash(fileNameBytes);

            return string.Format("{0}::{1}", hashContent, BitConverter.ToString(hash).Replace("-", "")).ToLower();
        }

        /// <summary>
        /// Devuelve una cadena de texto "md5" del contenido del archivo.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string MD5HashFile(string fileName)
        {
            byte[] fileContent = File.ReadAllBytes(fileName);
            byte[] hash = MD5.Create().ComputeHash(fileContent);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        private string runGit(string arguments)
        {

            Process p = new Process();

            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WorkingDirectory = this.folder;
            p.StartInfo.Arguments = arguments;

            p.StartInfo.FileName = "git";
            p.Start();
            
            string output = p.StandardOutput.ReadToEnd();

            p.WaitForExit();

            return output;
        }
    }
}
