using System;
using System.Data.SQLite;

namespace GlossaryNS {
    public class SQLiteConn: IDisposable {
        public string connstr;
        public SQLiteConnection conn;

        public SQLiteConn(string connstr) {
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

    }
}