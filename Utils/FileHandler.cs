using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace ZOPZ_SNIFF.Utils
{
    public class FileHandler
    {
        private static readonly string SettingDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF", "Settings");
        private static readonly string ExtraDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF", "Extra");

        public static void CheckDirectories()
        {
            if (!Directory.Exists(SettingDir))
                Directory.CreateDirectory(SettingDir);
            if (!Directory.Exists(ExtraDir))
                DownloadFiles("Extra.zip", ExtraDir).GetAwaiter().GetResult();
        }

        public static async Task DownloadFiles(string file, string extractPath)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] raw = await client.GetByteArrayAsync($"https://zopzsniff.xyz/assets/zopzfiles/{file}");
                string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.zip");
                File.WriteAllBytes(tempFilePath, raw);

                using (ZipArchive archive = ZipFile.OpenRead(tempFilePath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string relativePath = entry.FullName;
                        int index = relativePath.IndexOf('/');
                        if (index >= 0)
                            relativePath = relativePath.Substring(index + 1);

                        if (string.IsNullOrEmpty(relativePath))
                            continue;

                        string destinationPath = Path.Combine(extractPath, relativePath);

                        if (entry.Name == "") 
                        {
                            Directory.CreateDirectory(destinationPath);
                        }
                        else
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                            entry.ExtractToFile(destinationPath, overwrite: true);
                        }
                    }
                }

                File.Delete(tempFilePath);
            }
        }

        public static Image GetImage(string fileName)
        {
            using (Stream stream = new MemoryStream(File.ReadAllBytes(Path.Combine(ExtraDir, fileName))))
            {
                return Image.FromStream(stream);
            }
        }
    }
}
