using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Data.SQLite;

namespace GlossaryNS
{

    public class Glossary: IDisposable
    {
       
        public string connstr;
        public SQLiteConnection conn;

        public Glossary(string connstr) {
            this.connstr = connstr;
            conn = new SQLiteConnection(connstr);
            connect();
        }

        public void connect() {
            this.conn.Open();
        }

        public string getVersion() {
            using var cmd = new SQLiteCommand("SELECT SQLITE_VERSION()", conn);
            string version = cmd.ExecuteScalar().ToString();
            return version;
        }

        public void createTable() {
            using var cmd = new SQLiteCommand(conn);
            cmd.CommandText = @"CREATE TABLE glossary(k TEXT, v TEXT)";
            cmd.ExecuteNonQuery();
        }

        public void Dispose() {
            conn.Dispose();
        }

        // TODO concurrency
        // public void add(string s1, string s2)
        // {
        //     if (!dict.ContainsKey(s1))
        //     {
        //         dict[s1] = new List<string>();
        //     }
        //     dict[s1].Add(s2);
        // }

        // public List<String> query(string s)
        // {
        //     if (!dict.ContainsKey(s))
        //     {
        //         return new List<string>();
        //     }
        //     else
        //     {
        //         return dict[s];
        //     }
        // }
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

            // string dsstr = "Data Source=:memory:"
            var filename = "test.db";
            var currentDirectory = Directory.GetCurrentDirectory();
            var dsstr = $"URI=file:{Path.Combine(currentDirectory, filename)}";
            
            Console.WriteLine(dsstr);
            //string dsstr = "URI=file:.\test.db";
            Glossary conn = new Glossary(dsstr);
            Console.WriteLine($"SQLite version: {conn.getVersion()}");

            conn.createTable();
        }

    }
}
