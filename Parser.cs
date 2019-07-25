using System.Collections.Generic;

namespace myTree
{
    public static class Parser
    {
        public static List<string> parse(string[] args)
        {
            List<string> options = new List<string>();

            bool needInt = false;
            for (int i = 0; i < args.Length; i++)
            {
                if (needInt)
                {
                    bool isNumeric = int.TryParse(args[i], out int n);
                    if (isNumeric)
                    {
                        options.Add(n.ToString());
                    }
                    else
                    {
                        return null;
                    }
                    needInt = false;
                }
                else
                {
                    switch (args[i])
                    {
                        case "-d":
                            options.Add("-d");
                            needInt = true;
                            break;
                        case "--depth":
                            options.Add("-d");
                            needInt = true;
                            break;
                        case "-s":
                            options.Add("-s");
                            break;
                        case "--size":
                            options.Add("-s");
                            break;
                        case "-h":
                            options.Add("-h");
                            break;
                        case "--human-readable":
                            options.Add("-h");
                            break;
                        case "--help":
                            options.Add("--help");
                            break;
                        default: return null;
                    }
                }
            }
            return options;
        }
    }
}