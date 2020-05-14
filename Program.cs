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
        public const string tablename = "glossary";
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
            cmd.CommandText = $"CREATE TABLE {tablename}(k TEXT, v TEXT)";
            cmd.ExecuteNonQuery();
        }

        public void add(string k, string v) {
            using var cmd = new SQLiteCommand(conn);
            cmd.CommandText = $"INSERT INTO {tablename}(k, v) VALUES(@k, @v)";
            cmd.Parameters.AddWithValue("@k", k);
            cmd.Parameters.AddWithValue("@v", v);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public List<string> query(string k) {
            using var cmd = new SQLiteCommand($"SELECT * FROM {tablename} WHERE k = @k", conn);
            cmd.Parameters.AddWithValue("@k", k);
            cmd.Prepare();
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            var res = new List<string>();
            while (rdr.Read()) {
                res.Add(rdr.GetString(1));
            }
            return res;
        }

        public void Dispose() {
            conn.Dispose();
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

            // string dsstr = "Data Source=:memory:"
            var filename = "test.db";
            var currentDirectory = Directory.GetCurrentDirectory();
            var dsstr = $"URI=file:{Path.Combine(currentDirectory, filename)}";
            
            Console.WriteLine(dsstr);
            Glossary conn = new Glossary(dsstr);
            Console.WriteLine($"SQLite version: {conn.getVersion()}");

            conn.createTable();
            conn.add("srp", "storage resource provider");
            Console.WriteLine(conn.query("srp")[0]);
        }

    }
}
