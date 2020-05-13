using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Linq;

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
            // args
            if (args.Length < 2)
            {
                throw new System.ArgumentException("try these: \n" +
                                "    glossary add srp='storage resource provider' \n" +
                                "  or \n" +
                                "    glossary query srp");
            }
            Console.WriteLine(args[0]);
            Console.WriteLine(args[1]);

            // xml
            var filename = "glossary.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var purchaseOrderFilepath = Path.Combine(currentDirectory, filename);

            // no this file, create
            if (true)
            {
                XElement contacts =
                    new XElement("Contacts",
                        new XElement("Contact",
                            new XElement("Name", "Patrick Hines"),
                            new XElement("Phone", "206-555-0144",
                                new XAttribute("Type", "Home")),
                            new XElement("phone", "425-555-0145",
                                new XAttribute("Type", "Work")),
                            new XElement("Address",
                                new XElement("Street1", "123 Main St"),
                                new XElement("City", "Mercer Island"),
                                new XElement("State", "WA"),
                                new XElement("Postal", "68042")
                            )
                        )
                    );
            }

            //XElement purchaseOrder = XElement.Load(purchaseOrderFilepath);

            // IEnumerable<string> partNos = from item in purchaseOrder.Descendants("Item")
            //                               select (string)item.Attribute("PartNumber");
        }

    }
}
