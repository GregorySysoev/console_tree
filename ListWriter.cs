using myTree;
using System;
using System.Collections;
using System.Collections.Generic;

namespace myTree
{
    public class ListWriter : IWriter
    {
        public List<string> list;
        public string prefix;
        public void Write(string text)
        {
            if (list.Count == 0)
            {
                list.Add(text);
            }
            else
            {
                list[list.Count - 1] += text;
            }
            prefix = list[list.Count - 1];
        }
        public void WriteLine()
        {
            list.Add("");
            prefix = "";
        }

        public void WriteLine(string text)
        {
            list.Add(prefix + text);
            prefix = "";
        }

        public ListWriter()
        {
            list = new List<string>();
            prefix = "";
        }
    }
}