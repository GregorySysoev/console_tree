using System.Collections.Generic;

namespace myTree
{
    public static class Parser
    {
        public static void Parse(string[] args, out Options options)
        {
            bool _wasError = false;
            int _depth = -1;
            bool _needSize = false;
            bool _needHumanReadable = false;
            bool _needHelp = false;

            bool needInt = false;
            for (int i = 0; (i < args.Length) & (!_wasError); i++)
            {
                if (needInt)
                {
                    bool isNumeric = int.TryParse(args[i], out int n);
                    if (isNumeric)
                    {
                        _depth = n;
                    }
                    else
                    {
                        _wasError = true;
                    }
                    needInt = false;
                }
                else
                {
                    switch (args[i])
                    {
                        case "-d":
                        case "--depth":
                            needInt = true;
                            break;
                        case "-s":
                        case "--size":
                            _needSize = true;
                            break;
                        case "-h":
                        case "--human-readable":
                            _needHumanReadable = true;
                            break;
                        case "--help":
                            _needHelp = true;
                            break;
                        default:
                            _wasError = true;
                            break;
                    }
                }
            }

            if (needInt)
            {
                _wasError = true;
            }

            options = new Options(
                _wasError,
                _needSize,
                _needHumanReadable,
                _needHelp,
                _depth
            );
        }
    }
}