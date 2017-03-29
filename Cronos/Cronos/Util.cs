using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Cronos
{
    public static class Util
    {
        public static Tuple<string, string> DiscoverNamespace(string path)
        {
            Tuple<string, string> result = null;
            var folder = new DirectoryInfo(path);
            var directories = folder.GetDirectories();
            var domain = directories.Where(o => o.Name.Contains(".Domain")).Select(o => o.Name).FirstOrDefault();
            if (domain != null)
            {
                string[] slicedNamespace = domain.Split('.');
                if (slicedNamespace.Length > 2)
                {
                    result = new Tuple<string, string>(slicedNamespace[0], slicedNamespace[1]);
                }
                else
                {
                    throw new Exception("The selected folder is not referente to a ZEUS structure!");
                }
            }
            else
            {
                throw new Exception("The selected folder is not referente to a ZEUS structure!");
            }

            return result;
        }

        public static IList<DirectoryInfo> LoadOriginFolder(string path)
        {
            var folder = new DirectoryInfo(path);
            var directories = folder.GetDirectories();
            return directories.ToList();
        }

        public static void EditWriteFolder(string path, bool testPermission = false)
        {
            DirectoryInfo directorory = new DirectoryInfo(path);
            DirectorySecurity dSecurity = directorory.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
            directorory.SetAccessControl(dSecurity);

            ClearAttributes(path);

            if (testPermission)
            {
                var file = directorory.GetFiles().FirstOrDefault();
                if (file != null)
                {
                    string text = File.ReadAllText(file.FullName);
                    text = text + " ";
                    File.WriteAllText(file.FullName, text);
                }
            }           
        }

        public static void CopyFolders(string originPath, string destinyPath)
        {
            DirectoryCopy(originPath, destinyPath, true);
            EditWriteFolder(destinyPath);
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static void CleanDestiny(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            var directories = dir.GetDirectories();
            foreach (var item in directories)
            {
                item.Delete(true);
            }

            var files = dir.GetFiles();
            foreach (var item in files)
            {
                item.Delete();
            }
        }

        public static void ClearAttributes(string path)
        {
            if (Directory.Exists(path))
            {
                string[] subDirs = Directory.GetDirectories(path);
                foreach (string dir in subDirs)
                    ClearAttributes(dir);
                string[] files = files = Directory.GetFiles(path);
                foreach (string file in files)
                    File.SetAttributes(file, FileAttributes.Normal);
            }
        }

        public static string[] ZEUS_FOLDER = {
                "{namespaceAtual}.App",
                "{namespaceAtual}.Contract",
                "{namespaceAtual}.Data",
                "{namespaceAtual}.Domain",
                "{namespaceAtual}.Business",
                "{namespaceAtual}.Service",
                "{namespaceAtual}.UnitTest",
                "{namespaceAtual}.Util"
            };

        public static string[] IGNORE_FOLDER = {
                "App_Data",
                "Content",
                "Scripts",
                "fonts",
                "obj"
            };

        public static void RenameDestiny(string path, string currentNamespace, string newNamespace, string processFolder)
        {
            if (Directory.Exists(path))
            {
                var folder = new DirectoryInfo(path);

                var directories = !string.IsNullOrWhiteSpace(processFolder)
                                ? folder.GetDirectories().Where(o => o.Name == processFolder).ToList()
                                : folder.GetDirectories().Where(o => !IGNORE_FOLDER.Contains(o.Name)).ToList();

                foreach (var item in directories)
                {
                    var diretoriosFilhos = item.GetDirectories().Where(o => !IGNORE_FOLDER.Contains(o.Name)).ToList();
                    if (diretoriosFilhos.Count > 0)
                    {
                        RenameDestiny(item.FullName, currentNamespace, newNamespace, string.Empty);
                    }

                    if (item.Name.Contains(currentNamespace))
                    {
                        var novoNome = item.Name.Replace(currentNamespace, newNamespace);
                        var novoCaminho = item.FullName.Replace(item.Name, novoNome);
                        item.MoveTo(novoCaminho);
                    }

                    RenameFilesAndFolders(item, currentNamespace, newNamespace);
                }

                if (!string.IsNullOrWhiteSpace(processFolder))
                {
                    RenameFilesAndFolders(folder, currentNamespace, newNamespace);
                }
            }
        }

        public static void RenameFilesAndFolders(DirectoryInfo directory, string currentNamespace, string newNamespace)
        {
            var files = directory.GetFiles();
            foreach (var item in files)
            {
                if (item.Name.Contains(currentNamespace))
                {
                    var newName = item.Name.Replace(currentNamespace, newNamespace);
                    var newPath = item.FullName.Replace(item.Name, newName);
                    item.MoveTo(newPath);
                }

                if (item.Extension == ".sln" || item.Extension == ".gpState" || item.Extension == ".v11" || item.Extension == ".v12" || item.Extension == ".vssscc")
                {
                    var newName = newNamespace;
                    var newPath = item.FullName.Replace(item.Name, "") + newName + item.Extension;

                    if (!File.Exists(newPath))
                    {
                        item.MoveTo(newPath);
                    }
                }

                File.SetAttributes(item.FullName, FileAttributes.Normal);
                string text = File.ReadAllText(item.FullName);
                text = text.Replace(currentNamespace, newNamespace);
                File.WriteAllText(item.FullName, text);
            }
        }
    }
}
