using System.Collections.Generic;
using System.IO;
using System;

namespace myTree
{
    public class Printer
    {
        private static IWriter _writer;
        public static void Print(ref Options options, IWriter writer, string path)
        {
            _writer = writer;

            if (options.WasError)
            {
                _writer.WriteLine("Bad args");
                _writer.WriteLine("use --help");
            }
            else
            {
                List<int> pipes = new List<int>();
                if (options.NeedHelp) PrintHelp();
                else PrintRecursively(options.Depth, 0, path, ref pipes, ref options);
            }
        }

        public static void PrintRecursively(int depth, int indent, string path, ref List<int> pipes, ref Options options)
        {
            if (depth != 0)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileSystemInfo[] info = dir.GetFileSystemInfos();
                pipes.Add(indent);

                SortArray(ref info, options);

                for (int i = 0, localDepth = --depth; i < info.Length; i++)
                {
                    if (i == info.Length - 1)
                    {
                        pipes.Remove(indent);
                        PrintIndent(indent, ref pipes);
                        _writer.Write("└──");

                    }
                    else
                    {
                        PrintIndent(indent, ref pipes);
                        _writer.Write("├──");
                    }

                    if (info[i] is DirectoryInfo)
                    {
                        DirectoryInfo dInfo = (DirectoryInfo)info[i];
                        _writer.Write(dInfo.Name);
                        if (options.sorting.OrderByDateOfCreation)
                        {
                            _writer.Write(" " + dInfo.CreationTime.ToString());
                        }
                        else if (options.sorting.OrderByDateOfTransorm)
                        {
                            _writer.Write(" " + dInfo.CreationTime.ToString());
                        }
                        _writer.WriteLine();
                        PrintRecursively(localDepth, indent + 4, dInfo.FullName, ref pipes, ref options);
                    }
                    else
                    {
                        FileInfo fInfo = (FileInfo)info[i];
                        _writer.Write(fInfo.Name);
                        if (options.NeedHumanReadable | options.NeedSize)
                        {
                            _writer.Write(" " + ConvertBytesToHumanReadable(fInfo.Length, options.NeedHumanReadable));
                        }

                        if (options.sorting.OrderByDateOfCreation)
                        {
                            _writer.Write(" " + fInfo.CreationTime.ToString());
                        }
                        else if (options.sorting.OrderByDateOfTransorm)
                        {
                            _writer.Write(" " + fInfo.LastWriteTime.ToString());
                        }

                        _writer.WriteLine();
                    }
                }
                pipes.Remove(indent);
            }
        }
        public static void PrintHelp()
        {
            _writer.WriteLine();
            _writer.WriteLine("List of available commands:");
            _writer.WriteLine();
            _writer.WriteLine("-d --depth [num]  nesting level");
            _writer.WriteLine("-s --size  show size of files");
            _writer.WriteLine("-h --human-readable  show size of files in human-readable view {Bytes, KB, ...}");
            _writer.WriteLine("-o --order-by [flag] order of elements in tree");
            _writer.WriteLine();
            _writer.WriteLine("Available flags:");
            _writer.WriteLine("a - order by alphabet");
            _writer.WriteLine("s - order by size");
            _writer.WriteLine("t - order by time of last change");
            _writer.WriteLine("c - order by time of creation");
            _writer.WriteLine();

            _writer.WriteLine("--help show help");
            _writer.WriteLine();
            _writer.WriteLine("Example of using:");
            _writer.WriteLine("dotnet myTree.dll -d 5 -h -o a");
            _writer.WriteLine();
        }
        public static void PrintIndent(int indent, ref List<int> pipes)
        {
            for (int i = 0; i < indent; i++)
            {
                if (i % 4 == 0)
                {
                    if (pipes.Contains(i))
                    {
                        _writer.Write("│");
                    }
                }
                else
                {
                    _writer.Write(" ");
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
                return string.Format($"({num} Bytes)");
            }
            else
            {
                string[] suffixes =
    { "Bytes", "KB", "MB", "GB", "TB", "PB" };

                int counter = 0;
                decimal number = (decimal)num;
                while ((counter < 5) && (Math.Round(number / 1024) >= 1))
                {
                    number /= 1024;
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
        public static void SortArray(ref FileSystemInfo[] array, Options options)
        {
            if (options.sorting.OrderByAlphabet)
            {
                NameComparer nameComparer = new NameComparer();
                Array.Sort(array, nameComparer);
            }
            else if (options.sorting.OrderBySize)
            {
                SizeComparer sizeComparer = new SizeComparer();
                Array.Sort(array, sizeComparer);
            }
            else if (options.sorting.OrderByDateOfTransorm)
            {
                ChangeTimeComparer changeTimeComparer = new ChangeTimeComparer();
                Array.Sort(array, changeTimeComparer);
            }
            else if (options.sorting.OrderByDateOfCreation)
            {
                CreateTimeComparer createTimeComparer = new CreateTimeComparer();
                Array.Sort(array, createTimeComparer);
            }

            if (options.sorting.Reverse)
            {
                Array.Reverse(array);
            }
        }
    }

    public class NameComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo f1, FileSystemInfo f2)
        {
            return f1.Name.CompareTo(f2.Name);
        }
    }
    public class SizeComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo f1, FileSystemInfo f2)
        {
            if ((f1 is DirectoryInfo) && (f2 is DirectoryInfo))
            {
                return 0;
            }
            else if (f1 is DirectoryInfo)
            {
                return -1;
            }
            else if (f2 is DirectoryInfo)
            {
                return 1;
            }
            else
            {
                FileInfo first = (FileInfo)f1;
                FileInfo second = (FileInfo)f2;
                return first.Length.CompareTo(second.Length);
            }
        }
    }
    public class ChangeTimeComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo f1, FileSystemInfo f2)
        {
            return f1.LastWriteTime.CompareTo(f1.LastWriteTime);
        }
    }
    public class CreateTimeComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo f1, FileSystemInfo f2)
        {
            return f1.CreationTime.CompareTo(f2.CreationTime);
        }
    }
}