using System.Collections.Generic;
using System.IO;
using System;

namespace myTree
{
    public static class Printer
    {
        public static void Print(ref Options options)
        {

            if (options.WasError)
            {
                Console.WriteLine("Bad args");
                Console.WriteLine("use --help");
            }
            else
            {
                List<int> pipes = new List<int>();
                if (options.NeedHelp) PrintHelp();
                else PrintRecursively(options.Depth, 0, System.Environment.CurrentDirectory, ref pipes, ref options);
            }
        }

        public static void PrintRecursively(int depth, int indent, string path, ref List<int> pipes, ref Options options)
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
                        PrintRecursively(localDepth, indent + 4, dInfo.FullName, ref pipes, ref options);
                    }
                    else
                    {
                        FileInfo fInfo = (FileInfo)info[i];
                        Console.Write(fInfo.Name);
                        if (options.NeedHumanReadable | options.NeedSize) Console.Write(" " + ConvertBytesToHumanReadable(fInfo.Length, options.NeedHumanReadable));
                        Console.WriteLine();
                    }
                }
                pipes.Remove(indent);
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
            if (num == 0)
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

                if (number - decimal.Truncate(number) == 0)
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