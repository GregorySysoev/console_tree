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
            Printer.print(options);
        }

        public void execute()
        {

        }
    }
}