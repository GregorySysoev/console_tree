using System.Collections.Generic;
using System;

namespace myTree
{
    public class Algorithm
    {
        List<string> options = new List<string>();

        public Algorithm(string[] args)
        {
            options = Parser.parse(args);
        }

        public void execute()
        {
            Printer.print(options);
        }
    }
}