using System.Diagnostics;
using System.IO;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace StartESPDX
{
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Запуск программы...");

            //SartESPDXMT();        
            var url = "http://iammaddog.ru/cs2";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Начало загрузки файла...");
            Stopwatch downloadStopwatch = Stopwatch.StartNew();
            var data = await client.GetByteArrayAsync(url);
            downloadStopwatch.Stop();
            Console.WriteLine($"Файл успешно загружен за {downloadStopwatch.Elapsed.TotalSeconds} секунд.");

            await File.WriteAllBytesAsync("downloaded.rar", data);

            string rarPath = "downloaded.rar"; 
            string extractPath = @"C:\gagaga"; 
            string password = "666"; 

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Начало разархивирования файла...");
            Stopwatch extractionStopwatch = Stopwatch.StartNew();
            using (var archive = RarArchive.Open(rarPath, new ReaderOptions() { Password = password }))
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                    {
                        entry.WriteToDirectory(extractPath, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
            }
            extractionStopwatch.Stop();
            Console.WriteLine($"Файл успешно разархивирован за {extractionStopwatch.Elapsed.TotalSeconds} секунд.");

            File.Delete(rarPath);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Файл успешно извлечен и архив удален.");
            Console.ResetColor();
        }

        static void SartESPDXMT()
        {
            string folderPath = @"C:\gagaga"; 
            
            try
            {
                Process.Start(new ProcessStartInfo("steam://launch/730/Dialog") { UseShellExecute = true });

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Начало процеса cs2.exe");
                Stopwatch delayStopwatch = Stopwatch.StartNew();
                Thread.Sleep(21000); 
                
                delayStopwatch.Stop();
                Console.WriteLine($"Загрузка завершена за {delayStopwatch.Elapsed.TotalSeconds} секунд.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Не удалось запустить cs2.exe. Ошибка: {ex.Message}");
                Console.ResetColor();
            }

            
            foreach (string file in Directory.GetFiles(folderPath, "*.exe"))
            {
                
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = file,
                        UseShellExecute = true,
                        Verb = "runas"
                    };

                    try
                    {
                        Process.Start(startInfo);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Не удалось запустить файл {file}. Ошибка: {ex.Message}");
                        Console.ResetColor();
                    }
                
            }
        }


    }

}

    
