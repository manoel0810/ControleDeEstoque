using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Updater
{
    internal class Program
    {
        // Updater.exe (projeto separado)
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Uso: Updater.exe <path_zip> <path_destino> <app_para_reiniciar>");
                return;
            }

            string zipPath = args[0];
            string destinationPath = args[1];
            string appToStart = args[2];

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
                File.Delete(zipPath);
                Process.Start(appToStart);
            }
            catch (Exception ex)
            {
                File.WriteAllText("update_error.log", ex.ToString());
            }
        }

    }
}
