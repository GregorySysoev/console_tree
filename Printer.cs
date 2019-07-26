using System.Collections.Generic;
using System.IO;
using System;

namespace myTree
{
    public static class Printer
    {
        public static void print(List<string> options)
        {
            int Depth = -1;
            bool needSize = false;
            bool needHumanReadable = false;
            bool needHelp = false;

            if (options == null)
            {
                Console.WriteLine("Bad args");
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
                            Int32.TryParse(i, out Depth);
                            break;
                    }
                }
                if (needHelp) printHelp();
                else printRecurs(Depth, 0, System.Environment.CurrentDirectory, needSize, needHumanReadable);
            }
        }

        public static void printRecurs(int depth, int indent, string path,
        bool needSize, bool needHumanReadable)
        {
            if (depth != 0)
            {
                string[] dirs = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);

                for (int i = 0; i < files.Length; i++)
                {
                    printIndent(indent);
                    Console.Write(Path.GetFileName(files[i]));
                    if (needHumanReadable) Console.Write(" " + convertBytesToHumanReadable(new FileInfo(files[i]).Length));
                    else if (needSize) Console.Write(" (" + new FileInfo(files[i]).Length.ToString() + " Bytes)");
                    Console.WriteLine();
                }

                for (int i = 0, localDepth = --depth; i < dirs.Length; i++)
                {
                    printIndent(indent);
                    Console.WriteLine(new DirectoryInfo(dirs[i]).Name);
                    printRecurs(localDepth, indent + 4, (dirs[i]), needSize, needHumanReadable);
                }
            }
        }

        public static void printHelp()
        {
            Console.WriteLine("List of available commands:");
            Console.WriteLine();
            Console.WriteLine("-d --depth [num]  nesting level");
            Console.WriteLine("-s --size  show size of files");
            Console.WriteLine("--help  show help");
        }

        public static void printIndent(int indent)
        {
            for (int i = 0; i < indent; i++)
            {
                Console.Write(' ');
            }
        }

        public static string convertBytesToHumanReadable(long num)
        {
            string[] suffixes =
{ "Bytes", "KB", "MB", "GB", "TB", "PB" };
            int counter = 0;
            decimal number = (decimal)num;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("({0:n1} {1})", number, suffixes[counter]);
        }
    }
}