using System;
using System.IO;

namespace SearchAndCopyUpdates
{
    class Program
    {
        static void Main(string[] args)
        {
            //Argument 1 = Textdatei mit KB Nummern und mit Komma getrennt
            //Argument 2 = Ordner mit allen Updatedateien
            //Argument 3 = Ordner, wohin die gefundenen Updates kopiert werden sollen (Darf nicht mit "\" enden)
            try
            {
                if (args.Length > 1)
                {
                    var UpdatesRAWText = File.ReadAllText(args[0]);
                    var UpdateIDs = UpdatesRAWText.Split(',');
                    var AllFiles = Directory.GetFiles(args[1], "*.*", SearchOption.AllDirectories);
                    if (!Directory.Exists(args[2] + @"\x64"))
                    {
                        Directory.CreateDirectory(args[2] + @"\x64");
                    }
                    if (!Directory.Exists(args[2] + @"\x86"))
                    {
                        Directory.CreateDirectory(args[2] + @"\x86");
                    }
                    for (int i = 0; i < AllFiles.Length; i++)
                    {
                        for (int i2 = 0; i2 < UpdateIDs.Length; i2++)
                        {
                            if (AllFiles[i].ToLowerInvariant().Contains(UpdateIDs[i2].ToLowerInvariant()))
                            {
                                if (AllFiles[i].ToLowerInvariant().Contains("x86"))
                                {
                                    File.Copy(AllFiles[i], args[2] + @"\x86\" + Path.GetFileName(AllFiles[i]));
                                }
                                else
                                {
                                    File.Copy(AllFiles[i], args[2] + @"\x64\" + Path.GetFileName(AllFiles[i]));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
