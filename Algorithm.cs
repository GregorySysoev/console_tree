using System.Collections.Generic;
using System;

namespace myTree
{
    public class Options
    {
        private bool wasError { get; }
        public bool WasError => wasError;

        private bool needHumanReadable { get; }
        public bool NeedHumanReadable => needHumanReadable;

        private bool needSize { get; }
        public bool NeedSize => needSize;

        private bool needHelp { get; }
        public bool NeedHelp => needHelp;

        private int depth { get; }
        public int Depth => depth;

        public Options(bool _wasError, bool _needSize, bool _needHuman_readable, bool _needHelp, int _depth)
        {
            wasError = _wasError;
            needSize = _needSize;
            needHumanReadable = _needHuman_readable;
            needHelp = _needHelp;
            depth = _depth;
        }
    }
    public class Algorithm
    {
        Options options;

        public Algorithm(string[] args)
        {
            Parser.Parse(args, out options);
        }
        public void execute()
        {
            Printer.Print(ref options);
        }
    }
}