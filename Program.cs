using System;
using System.Collections.Generic;



namespace GlossaryNS
{

    public class Glossary
    {
        Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        // TODO concurrency
        public void add(string s1, string s2)
        {
            if (!dict.ContainsKey(s1))
            {
                dict[s1] = new List<string>();
            }
            dict[s1].Add(s2);
        }

        public List<String> query(string s)
        {
            if (!dict.ContainsKey(s))
            {
                return new List<string>();
            }
            else
            {
                return dict[s];
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2) {
                throw new System.ArgumentException("try these: \n" +  
                                "    glossary add srp='storage resource provider' \n" + 
                                "  or \n" +
                                "    glossary query srp");
            }
            Console.WriteLine(args[0]);
            Console.WriteLine(args[1]);
        }
        
    }
}
