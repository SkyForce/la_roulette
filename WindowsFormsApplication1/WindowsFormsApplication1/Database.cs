using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace roulette
{
    class Database
    {
        SQLiteConnection conn;
        string path;
        public Database(string path)
        {
            this.path = path;
            conn = new SQLiteConnection("Data Source=" + path + ";");
            conn.Open();
        }

        public void SaveBase(String p)
        {
            File.Copy(path, p, true);
        }

        public int Create(String login, String pass)
        {
            try
            {
                SQLiteCommand comm = new SQLiteCommand(String.Format("INSERT INTO data (login, password) VALUES ('{0}', '{1}')", login, pass), conn);
                comm.ExecuteNonQuery();
                comm = new SQLiteCommand(String.Format("SELECT id FROM data WHERE login = \"{0}\"", login), conn);
                SQLiteDataReader r = comm.ExecuteReader();
                while (r.Read())
                {
                    return r.GetInt32(0);
                }
            }
            catch (SQLiteException e)
            {
                throw new Exception("login is already used");
            }
            throw new Exception("auth error");
            //conn.Close();
        }

        public int Update(int id, int delta, bool isDemo)
        {
            SQLiteCommand comm = new SQLiteCommand(String.Format("UPDATE 'data' SET {2} = {2} + {0} WHERE id = {1}", delta, id, isDemo ? "money_demo" : "money_normal"), conn);
            comm.ExecuteNonQuery();
            comm = new SQLiteCommand("SELECT * FROM 'data' WHERE id = " + id, conn);
            SQLiteDataReader r = comm.ExecuteReader();
            while (r.Read())
            {
                return isDemo ? r.GetInt32(4) : r.GetInt32(3);
            }
            throw new Exception("can't update data");
        }

        public int Auth(string p1, string p2)
        {
            SQLiteCommand comm = new SQLiteCommand(String.Format("SELECT id, password  FROM 'data' WHERE login = \"{0}\"", p1), conn);
            SQLiteDataReader r = comm.ExecuteReader();
            while(r.Read())
            {
                if (r.GetString(1) == p2)
                {
                    return (int)r.GetInt32(0);
                }
            }
            throw new Exception("incorrect login or password");
        }
    }
}
