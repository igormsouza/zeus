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
        public static Tuple<string, string> DescobreNamespace(string caminho)
        {
            Tuple<string, string> retorno = null;
            var pasta = new DirectoryInfo(caminho);
            var diretorios = pasta.GetDirectories();
            var dominio = diretorios.Where(o => o.Name.Contains(".Dominio")).Select(o => o.Name).FirstOrDefault();
            if (dominio != null)
            {
                string[] namespacePartido = dominio.Split('.');
                if (namespacePartido.Length > 2)
                {
                    retorno = new Tuple<string, string>(namespacePartido[0], namespacePartido[1]);
                }
                else
                {
                    throw new Exception("Pasta Selecionada não é referente a base ZEUS!");
                }
            }
            else
            {
                throw new Exception("Pasta Selecionada não é referente a base ZEUS!");
            }

            return retorno;
        }

        public static IList<DirectoryInfo> CarregaPastasOrigem(string caminho)
        {
            var pasta = new DirectoryInfo(caminho);
            var diretorios = pasta.GetDirectories();
            return diretorios.ToList();
        }

        public static void AlteraPermissaoDeEscritaPasta(string caminho, bool testePermissao = false)
        {
            DirectoryInfo diretorio = new DirectoryInfo(caminho);
            DirectorySecurity dSecurity = diretorio.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
            diretorio.SetAccessControl(dSecurity);

            ClearAttributes(caminho);

            if (testePermissao)
            {
                var arquivo = diretorio.GetFiles().FirstOrDefault();
                if (arquivo != null)
                {
                    string text = File.ReadAllText(arquivo.FullName);
                    text = text + " ";
                    File.WriteAllText(arquivo.FullName, text);
                }
            }           
        }

        public static void CopiaArquivosPastaOrigemParaDestino(string caminhoOrigem, string caminhoDestino)
        {
            DirectoryCopy(caminhoOrigem, caminhoDestino, true);
            AlteraPermissaoDeEscritaPasta(caminhoDestino);
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

        public static void LimpaDestino(string caminho)
        {
            DirectoryInfo dir = new DirectoryInfo(caminho);
            var diretorios = dir.GetDirectories();
            foreach (var item in diretorios)
            {
                item.Delete(true);
            }

            var arquivos = dir.GetFiles();
            foreach (var item in arquivos)
            {
                item.Delete();
            }
        }

        public static void ClearAttributes(string caminho)
        {
            if (Directory.Exists(caminho))
            {
                string[] subDirs = Directory.GetDirectories(caminho);
                foreach (string dir in subDirs)
                    ClearAttributes(dir);
                string[] files = files = Directory.GetFiles(caminho);
                foreach (string file in files)
                    File.SetAttributes(file, FileAttributes.Normal);
            }
        }

        public static string[] pastasZeus = {
                "{namespaceAtual}.App",
                "{namespaceAtual}.Contrato",
                "{namespaceAtual}.Dados",
                "{namespaceAtual}.Dominio",
                "{namespaceAtual}.Negocio",
                "{namespaceAtual}.Servico",
                "{namespaceAtual}.TesteUnitario",
                "{namespaceAtual}.Util"
            };

        public static string[] pastasInternasIgnorar = {
                "App_Data",
                "Content",
                "Scripts",
                "fonts",
                "obj"
            };

        public static void RenomeiaDestino(string caminho, string namespaceAtual, string namespaceFuturo, string pastaProcessada)
        {
            if (Directory.Exists(caminho))
            {
                var pasta = new DirectoryInfo(caminho);

                var diretorios = !string.IsNullOrWhiteSpace(pastaProcessada)
                                ? pasta.GetDirectories().Where(o => o.Name == pastaProcessada).ToList()
                                : pasta.GetDirectories().Where(o => !pastasInternasIgnorar.Contains(o.Name)).ToList();

                foreach (var item in diretorios)
                {
                    var diretoriosFilhos = item.GetDirectories().Where(o => !pastasInternasIgnorar.Contains(o.Name)).ToList();
                    if (diretoriosFilhos.Count > 0)
                    {
                        RenomeiaDestino(item.FullName, namespaceAtual, namespaceFuturo, string.Empty);
                    }

                    if (item.Name.Contains(namespaceAtual))
                    {
                        var novoNome = item.Name.Replace(namespaceAtual, namespaceFuturo);
                        var novoCaminho = item.FullName.Replace(item.Name, novoNome);
                        item.MoveTo(novoCaminho);
                    }

                    RenomeiaArquivosPasta(item, namespaceAtual, namespaceFuturo);
                }

                if (!string.IsNullOrWhiteSpace(pastaProcessada))
                {
                    RenomeiaArquivosPasta(pasta, namespaceAtual, namespaceFuturo);
                }
            }
        }

        public static void RenomeiaArquivosPasta(DirectoryInfo diretorio, string namespaceAtual, string namespaceFuturo)
        {
            var arquivos = diretorio.GetFiles();
            foreach (var item in arquivos)
            {
                if (item.Name.Contains(namespaceAtual))
                {
                    var novoNome = item.Name.Replace(namespaceAtual, namespaceFuturo);
                    var novoCaminho = item.FullName.Replace(item.Name, novoNome);
                    item.MoveTo(novoCaminho);
                }

                if (item.Extension == ".sln" || item.Extension == ".gpState" || item.Extension == ".v11" || item.Extension == ".v12" || item.Extension == ".vssscc")
                {
                    var novoNome = namespaceFuturo;
                    var novoCaminho = item.FullName.Replace(item.Name, "") + novoNome + item.Extension;

                    if (!File.Exists(novoCaminho))
                    {
                        item.MoveTo(novoCaminho);
                    }
                }

                File.SetAttributes(item.FullName, FileAttributes.Normal);
                string text = File.ReadAllText(item.FullName);
                text = text.Replace(namespaceAtual, namespaceFuturo);
                File.WriteAllText(item.FullName, text);
            }
        }
    }
}
