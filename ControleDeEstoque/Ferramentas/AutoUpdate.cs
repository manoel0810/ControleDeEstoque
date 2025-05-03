using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace Ferramentas
{
    public class AutoUpdate
    {
        private const string _versionFile = "update_version.txt";
        private const string _downloadedZipPath = "update.zip";
        private const string _pendingFlag = "update_pending.flag";

        private readonly Version _currentVersion;
        private readonly string _sourceUrl;

        public AutoUpdate(string currentVersion, string sourceUrl)
        {
            _currentVersion = new Version(currentVersion);
            _sourceUrl = sourceUrl;
        }

        public event Action<string, string> AtualizacaoDisponivel; // versão, changelog
        public event Action AtualizacaoJaBaixada;
        public event Action<string> Erro;
        public event Action AtualizacaoAplicada;

        public void VerificarAtualizacaoEmSegundoPlano()
        {
            Thread thread = new Thread(VerificarAtualizacao)
            {
                IsBackground = true
            };
            thread.Start();
        }

        public void AplicarAtualizacaoComAuxiliar()
        {
            try
            {
                string updaterExe = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Updater.exe");

                if (!File.Exists(updaterExe))
                {
                    Erro?.Invoke("O arquivo 'Updater.exe' não foi encontrado.");
                    return;
                }

                string zipPath = Path.GetFullPath(_downloadedZipPath);
                string appPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;

                string args = $"\"{zipPath}\" \"{baseDir}\" \"{appPath}\"";

                Process.Start(updaterExe, args);
                Environment.Exit(0); // Encerra o app atual para permitir atualização
            }
            catch (Exception ex)
            {
                Erro?.Invoke("Erro ao iniciar o atualizador: " + ex.Message);
            }
        }

        private void VerificarAtualizacao()
        {
            try
            {
                AutoUpdateConfig config = BaixarConfiguracao();
                if (config == null)
                    return;

                Version novaVersao = new Version(config.Version);
                if (_currentVersion >= novaVersao)
                {
                    if (File.Exists(_pendingFlag))
                        AtualizacaoJaBaixada?.Invoke();
                    return;
                }

                if (File.Exists(_downloadedZipPath) && LerVersaoLocal() == config.Version)
                {
                    AtualizacaoJaBaixada?.Invoke();
                    return;
                }

                if (BaixarArquivo(config.Url, _downloadedZipPath))
                {
                    SalvarVersaoLocal(config.Version);
                    File.WriteAllText(_pendingFlag, "pendente");
                    AtualizacaoDisponivel?.Invoke(config.Version, config.Changelog);
                }
            }
            catch (Exception ex)
            {
                Erro?.Invoke(ex.Message);
            }
        }

        private AutoUpdateConfig BaixarConfiguracao()
        {
            using (var client = new WebClient())
            {
                string json = client.DownloadString(_sourceUrl);
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    var serializer = new DataContractJsonSerializer(typeof(AutoUpdateConfig));
                    return (AutoUpdateConfig)serializer.ReadObject(ms);
                }
            }
        }

        private bool BaixarArquivo(string url, string destino)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, destino);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void AplicarAtualizacao()
        {
            try
            {
                string tempExtractPath = Path.Combine(Path.GetTempPath(), "app_update_temp");
                if (Directory.Exists(tempExtractPath))
                    Directory.Delete(tempExtractPath, true);

                ZipFile.ExtractToDirectory(_downloadedZipPath, tempExtractPath);

                foreach (string filePath in Directory.GetFiles(tempExtractPath, "*", SearchOption.AllDirectories))
                {
                    string relativePath = filePath.Substring(tempExtractPath.Length + 1);
                    string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                    File.Copy(filePath, destinationPath, true);
                }

                // Limpeza
                File.Delete(_downloadedZipPath);
                File.Delete(_versionFile);
                File.Delete(_pendingFlag);

                AtualizacaoAplicada?.Invoke();
            }
            catch (Exception ex)
            {
                Erro?.Invoke(ex.Message);
            }
        }

        private void SalvarVersaoLocal(string version)
        {
            File.WriteAllText(_versionFile, version);
        }

        private string LerVersaoLocal()
        {
            if (!File.Exists(_versionFile)) return null;
            return File.ReadAllText(_versionFile).Trim();
        }
    }

    [Serializable]
    public class AutoUpdateConfig
    {
        public string Version { get; set; }
        public string Url { get; set; }
        public string Changelog { get; set; }
        public string ReleaseDate { get; set; }
    }
}
