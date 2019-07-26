using System.Collections.Generic;
using System;

namespace myTree
{
    public class Algorithm
    {
        List<string> options = new List<string>();

        public Algorithm(string[] args)
        {
            options = Parser.Parse(args);
        }

        public void execute()
        {
            Printer.Print(options);
        }
    }
}