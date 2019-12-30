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

            bool success = true;
            foreach (string path in args)
            {
                bool status = ConvertFile(path);
                if (status == false) success = false;
            }

            if (success == false) Console.ReadLine();
        }

        static bool ConvertFile(string path)
        {
            bool fileExists = File.Exists(path);
            if (fileExists == false)
            {
                Console.WriteLine($"File \"{path}\" does not exist");
                return false;
            }

            using (StreamReader file = File.OpenText(path))
            {
                try
                {
                    var legacy = PlaylistConverter.DeserializeLegacyPlaylist(file);
                    var playlist = PlaylistConverter.ConvertLegacyPlaylist(legacy);

                    string newPath = path.Replace(".bplist", ".blist").Replace(".json", ".blist");
                    using FileStream fs = File.Open(newPath, FileMode.OpenOrCreate);
                    using MemoryStream ms = PlaylistLib.SerializeStream(playlist);

                    ms.CopyTo(fs);
                    fs.Flush();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in {path}");
                    Console.WriteLine(ex.ToString());
                    return false;
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
