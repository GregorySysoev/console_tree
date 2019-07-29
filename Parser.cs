using System.Collections.Generic;

namespace myTree
{
    public static class Parser
    {
        public static void Parse(string[] args, out Options options)
        {
            bool wasError = false;
            int depth = -1;
            bool needSize = false;
            bool needHumanReadable = false;
            bool needHelp = false;

            bool orderByAlphabet = false;
            bool orderBySize = false;
            bool orderByDateOfTransorm = false;
            bool orderByDateOfCreation = false;
            bool orderReverse = false;

            bool needInt = false;
            bool needFlag = false;
            for (int i = 0; (i < args.Length) & (!wasError); i++)
            {
                if (needInt)
                {
                    bool isNumeric = int.TryParse(args[i], out int n);
                    if (isNumeric)
                    {
                        depth = n;
                    }
                    else
                    {
                        wasError = true;
                    }
                    needInt = false;
                }
                else if (!needFlag)
                {
                    switch (args[i])
                    {
                        case "-d":
                        case "--depth":
                            needInt = true;
                            break;
                        case "-s":
                        case "--size":
                            needSize = true;
                            break;
                        case "-h":
                        case "--human-readable":
                            needHumanReadable = true;
                            break;
                        case "--help":
                            needHelp = true;
                            break;
                        case "-o":
                        case "--order-by":
                            needFlag = true;
                            break;
                        case "-r":
                        case "--reverse":
                            orderReverse = true;
                            break;
                        default:
                            wasError = true;
                            break;
                    }
                }
                else
                {
                    switch (args[i])
                    {
                        case "a":
                            orderByAlphabet = true;
                            needFlag = false;
                            break;
                        case "s":
                            orderBySize = true;
                            break;
                        case "t":
                            orderByDateOfTransorm = true;
                            break;
                        case "c":
                            orderByDateOfCreation = true;
                            break;
                        default:
                            wasError = true;
                            break;
                    }
                    needFlag = false;
                }
            }

            if ((needInt) | (needFlag))
            {
                wasError = true;
            }

            options = new Options(
                wasError,
                needSize,
                needHumanReadable,
                needHelp,
                depth,
                orderByAlphabet,
                orderBySize,
                orderByDateOfTransorm,
                orderByDateOfCreation,
                orderReverse
            );
        }
    }
}