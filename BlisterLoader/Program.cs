using System;
using System.IO;
using Blister;
using Blister.Conversion;

namespace BlisterLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ExitMessage("Please drag a .bplist file onto this .exe", 1);
            }

            string path = args[0];
            bool fileExists = File.Exists(path);
            if (!fileExists)
            {
                ExitMessage("The specified file does not exist!", 1);
            }

            string text = File.ReadAllText(path);
            var legacy = PlaylistConverter.LoadLegacyPlaylist(text);
            var playlist = PlaylistConverter.ConvertLegacyPlaylist(legacy);

            string newPath = path.Replace(".bplist", ".blist");
            using (FileStream fs = File.Open(newPath, FileMode.OpenOrCreate))
            {
                using (MemoryStream ms = PlaylistLib.SaveStream(playlist))
                {
                    ms.CopyTo(fs);
                    fs.Flush();
                }
            }
        }

        static void ExitMessage(string message, int exitCode)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press [ENTER] to exit...");
            Console.ReadKey();

            Environment.Exit(exitCode);
        }
    }
}
