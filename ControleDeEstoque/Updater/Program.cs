using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Updater
{
    internal class Program
    {
        private const string _versionFile = "update_version.txt";
        private const string _downloadedZipPath = "update.zip";
        private const string _pendingFlag = "update_pending.flag";

        static void Main(string[] args)
        {
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.WriteLine("Arquivo de parâmetros não encontrado.");
                return;
            }

            string json = File.ReadAllText(args[0]);
            var parametros = JsonConvert.DeserializeObject<ParametrosUpdate>(json);

            string zipPath = parametros.ZipPath;
            string destinationPath = parametros.Destino;
            string appToStart = parametros.Executavel;

            Thread.Sleep(2000); // Aguarda app fechar

            try
            {
                if (Directory.Exists("temp_update"))
                    Directory.Delete("temp_update", true);

                ZipFile.ExtractToDirectory(zipPath, "temp_update");

                foreach (string file in Directory.GetFiles("temp_update", "*", SearchOption.AllDirectories))
                {
                    string relative = file.Substring("temp_update".Length + 1);
                    string target = Path.Combine(destinationPath, relative);
                    Directory.CreateDirectory(Path.GetDirectoryName(target));
                    File.Copy(file, target, true);
                }

                Directory.Delete("temp_update", true);
                File.Delete(_downloadedZipPath);
                File.Delete(_versionFile);
                File.Delete(_pendingFlag);

                File.Delete(zipPath);
                Process.Start(appToStart);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                string dados = $"Erro ao aplicar atualização: {ex.Message}\nARGS: {string.Join("\n", args)}";
                File.WriteAllText("update_error.log", dados);
            }
        }

        class ParametrosUpdate
        {
            public string ZipPath { get; set; }
            public string Destino { get; set; }
            public string Executavel { get; set; }
        }

    }
}
