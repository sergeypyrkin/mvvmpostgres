using Npgsql;
using PostGressGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace npgsqlProject
{
    public class Connector
    {

        public static string conString = String.Format("Server={0};Port={1};Database={2};User Id={3};Password={4}", "localhost", 5432, "localdb", "postgres", "admin");
        public NpgsqlConnection conn = null;
        private string sql;
        private NpgsqlCommand cmd;
        private System.Data.DataTable dt;

        public Connector()
        {
            getCon();
        }

        public NpgsqlConnection getCon()
        {
            conn = new NpgsqlConnection(conString);
            return conn;
        }

        public List<User> Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from users";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new System.Data.DataTable();
                dt.Load(cmd.ExecuteReader());
                List<User> users = getUsersByDataSet(dt);
                if (conn != null)
                {
                    conn.Close();
                }
                return users;
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    conn.Close();
                }
                MessageBox.Show("connection error: " + ex.Message);
                return null;
            }
        }


        public List<User> getUsersByDataSet(System.Data.DataTable dt)
        {
            List<User> users = new List<User>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var name = dt.Rows[i]["name"].ToString();
                var tid = dt.Rows[i]["id"].ToString();
                var tage = dt.Rows[i]["age"].ToString();
                int id = Convert.ToInt32(tid);
                int age = Convert.ToInt32(tage);
                User user = new User();
                user.id = id;
                user.name = name;
                user.age = age;
                users.Add(user);
            }
            return users;
        }

        internal void Insert(User u)
        {
            try
            {
                conn.Open();
                sql = $"insert into users(id, name, age) VALUES(@id, @name, @age)";
                var cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", u.id);
                cmd.Parameters.AddWithValue("@name", u.name);
                cmd.Parameters.AddWithValue("@age", u.age);

                var result = cmd.ExecuteNonQuery();
                if (conn != null)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    conn.Close();
                }
                MessageBox.Show("connection error: " + ex.Message);
            }
        }

        internal void remove(User u)
        {
            try
            {
                conn.Open();
                sql = $"delete from users where id={u.id};";
                var cmd = new NpgsqlCommand(sql, conn);
                var result = cmd.ExecuteNonQuery();
                if (conn != null)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    conn.Close();
                }
                MessageBox.Show("connection error: " + ex.Message);
            }
        }
    }
}
