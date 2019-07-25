using System.Collections.Generic;
using System;

namespace myTree
{
    public static class Printer
    {
        public static void print(List<string> options)
        {
            int hasDepth = 0; // если есть то не равно нулю
            bool needSize = false;
            bool needHumanReadable = false;

            if (options == null)
            {
                Console.WriteLine("Bad args");
            }
            else
            {
                foreach (var i in options)
                {
                    switch (i): { }
                }
            }
        }
    }
}
}