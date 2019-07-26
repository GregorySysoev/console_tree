using System;
using System.Collections;
using System.Collections.Generic;
using myTree;

namespace myTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Algorithm algorithm = new Algorithm(args);
            algorithm.execute();
        }
    }
}
