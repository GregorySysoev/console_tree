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
            List<int> pipes = new List<int>();

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
                else PrintRecurs(depth, 0, System.Environment.CurrentDirectory, needSize, needHumanReadable, ref pipes/* new List<int>()*/);
            }
        }

        public static void PrintRecurs(int depth, int indent, string path,
        bool needSize, bool needHumanReadable, ref List<int> pipes)
        {
            if (depth != 0)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileSystemInfo[] info = dir.GetFileSystemInfos();
                pipes.Add(indent);

                for (int i = 0, localDepth = --depth; i < info.Length; i++)
                {
                    if (i == info.Length - 1)
                    {
                        pipes.Remove(indent);
                        PrintIndent(indent, ref pipes);
                        Console.Write("└──");

                    }
                    else
                    {
                        PrintIndent(indent, ref pipes);
                        Console.Write("├──");
                    }

                    if (info[i] is DirectoryInfo)
                    {
                        DirectoryInfo dInfo = (DirectoryInfo)info[i];
                        Console.WriteLine(dInfo.Name);
                        PrintRecurs(localDepth, indent + 4, dInfo.FullName, needSize, needHumanReadable, ref pipes);
                    }
                    else
                    {
                        FileInfo fInfo = (FileInfo)info[i];
                        Console.Write(fInfo.Name);
                        if (needHumanReadable | needSize) Console.Write(" " + ConvertBytesToHumanReadable(fInfo.Length, needHumanReadable));
                        Console.WriteLine();
                    }
                }
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


        public static void PrintIndent(int indent, ref List<int> pipes)
        {
            for (int i = 0; i < indent; i++)
            {
                if (i % 4 == 0)
                {
                    if (pipes.Contains(i))
                    {
                        Console.Write("│");
                    }
                }
                else
                {
                    Console.Write(" ");
                }
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