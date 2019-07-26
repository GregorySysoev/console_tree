using System.Collections.Generic;
using System.IO;
using System;

namespace myTree
{
    public static class Printer
    {
        public static void Print(List<string> options /* TODO */)
        {
            int depth = -1;
            bool needSize = false;
            bool needHumanReadable = false;
            bool needHelp = false;

            if (options == null)
            {
                Console.WriteLine("Bad args");
                Console.WriteLine("use --help");
            }
            else
            {
                foreach (var i in options)
                {
                    switch (i)
                    {
                        case ("-s"):
                            needSize = true;
                            break;
                        case ("-h"):
                            needHumanReadable = true;
                            break;
                        case ("--help"):
                            needHelp = true;
                            break;
                        case ("-d"):
                            break;
                        default:
                            Int32.TryParse(i, out depth);
                            break;
                    }
                }
                if (needHelp) PrintHelp();
                else PrintRecurs(depth, 0, System.Environment.CurrentDirectory, needSize, needHumanReadable);
            }
        }

        public static void PrintRecurs(int depth, int indent, string path,
        bool needSize, bool needHumanReadable)
        {
            if (depth != 0)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileSystemInfo[] info = dir.GetFileSystemInfos();

                int localDepth = --depth;
                foreach (FileSystemInfo i in info)
                {

                    if (i is DirectoryInfo)
                    {
                        PrintIndent(indent);
                        DirectoryInfo dInfo = (DirectoryInfo)i;
                        Console.WriteLine(dInfo.Name);
                        PrintRecurs(localDepth, indent + 4, dInfo.FullName, needSize, needHumanReadable);
                    }
                    else
                    {
                        PrintIndent(indent);
                        FileInfo fInfo = (FileInfo)i;
                        Console.Write(fInfo.Name);
                        if (needHumanReadable | needSize) Console.Write(" " + ConvertBytesToHumanReadable(fInfo.Length, needHumanReadable));
                        Console.WriteLine();
                    }
                }

                // string[] dirs = Directory.GetDirectories(path); // TODO
                // string[] files = Directory.GetFiles(path);

                // for (int i = 0; i < files.Length; i++)
                // {
                //     PrintIndent(indent);
                //     Console.Write(Path.GetFileName(files[i]));
                //     if (needHumanReadable | needSize) Console.Write(" " + ConvertBytesToHumanReadable(new FileInfo(files[i]).Length, needHumanReadable));
                //     Console.WriteLine();
                // }

                // for (int i = 0, localDepth = --depth; i < dirs.Length; i++)
                // {
                //     PrintIndent(indent);
                //     Console.WriteLine(new DirectoryInfo(dirs[i]).Name);
                //     PrintRecurs(localDepth, indent + 4, (dirs[i]), needSize, needHumanReadable);
                // }
            }
        }

        public static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("List of available commands:");
            Console.WriteLine();
            Console.WriteLine("-d --depth [num]  nesting level");
            Console.WriteLine("-s --size  show size of files");
            Console.WriteLine("-h --human-readable  show size of files in human-readable view {Bytes, KB, ...}");
            Console.WriteLine("--help show help");
            Console.WriteLine();
            Console.WriteLine("Example of using:");
            Console.WriteLine("dotnet myTree.dll -d 5 -h");
            Console.WriteLine();
        }

        public static void PrintIndent(int indent)
        {
            for (int i = 0; i < indent; i++)
            {
                Console.Write(' ');
            }
        }

        public static string ConvertBytesToHumanReadable(long num, bool humanRead)
        {
            if (num == 0) // Ничего что несколько return'ов?
            {
                return ("(empty)");
            }
            if (!humanRead)
            {
                return string.Format("({0} {1})", num, "Bytes");
            }
            else
            {
                string[] suffixes =
    { "Bytes", "KB", "MB", "GB", "TB", "PB" };

                int counter = 0;
                decimal number = (decimal)num;
                while (!suffixes[counter].Equals("PB") && (Math.Round(number / 1024) >= 1))
                {
                    number = number / 1024;
                    counter++;
                }

                if (number - decimal.Truncate(number) == 0) // Не нашёл у Decimal метода, чтобы получить только дробную часть.
                {
                    return string.Format("({0:} {1})", number, suffixes[counter]);
                }
                else
                {
                    return string.Format("({0:n1} {1})", number, suffixes[counter]);
                }
            }
        }
    }
}