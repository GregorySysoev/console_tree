using System.Collections.Generic;
using System;

namespace myTree
{
    public class Options
    {
        // Общие параметры
        public bool WasError { get; }
        public bool NeedHumanReadable { get; }
        public bool NeedSize { get; }
        public bool NeedHelp { get; }
        public int Depth { get; }

        public struct Sorting
        {
            public bool OrderByAlphabet { get; }
            public bool OrderBySize { get; }
            public bool OrderByDateOfTransorm { get; }
            public bool OrderByDateOfCreation { get; }
            public bool Reverse { get; }

            public Sorting(bool orderByAlphabet, bool orderBySize, bool orderByDateOfTransorm, bool orderByDateOfCreation, bool reverse)
            {
                OrderByAlphabet = orderByAlphabet;
                OrderBySize = orderBySize;
                OrderByDateOfTransorm = orderByDateOfTransorm;
                OrderByDateOfCreation = orderByDateOfCreation;
                Reverse = reverse;
            }
        }

        public Sorting sorting;
        public Options(bool wasError, bool needSize, bool needHumanReadable, bool needHelp,
        int depth, bool orderByAlphabet, bool orderBySize, bool orderByDateOfTransorm,
        bool orderByDateOfCreation, bool reverse)
        {
            WasError = wasError;
            NeedSize = needSize;
            NeedHumanReadable = needHumanReadable;
            NeedHelp = needHelp;
            Depth = depth;

            sorting = new Sorting(orderByAlphabet, orderBySize, orderByDateOfTransorm, orderByDateOfCreation, reverse);
        }
    }
    public class Algorithm
    {
        Options options;

        public Algorithm(string[] args)
        {
            Parser.Parse(args, out options);
        }
        public void Execute()
        {
            Printer.Print(ref options);
        }
    }
}