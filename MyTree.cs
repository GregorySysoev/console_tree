using System.IO;
using System;

namespace myTree
{
    public class MyTree
    {
        private int depth = 0;
        public void printTree()
        {
            printRec(0, System.Environment.CurrentDirectory);
        }
        void printRec(int level, string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < dirs.Length; i++)
            {
                printIndent(level);
                Console.WriteLine(new DirectoryInfo(dirs[i]).Name);
                printRec(++level, (dirs[i]));
            }

            for (int i = 0; i < files.Length; i++)
            {
                printIndent(level);
                Console.WriteLine(Path.GetFileName(files[i]));
            }
        }

        void printIndent(int level)
        {
            for (int i = 0; i < level * 2; i++)
            {
                Console.Write(' ');
            }
        }
        void printFile()
        {

        }
        public MyTree(/* int _depth*/)
        {
            // depth = _depth;
        }
    }
}