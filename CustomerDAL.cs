using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BOL;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class CustomerDAL
    {
        public static string connectionstring = @"server=localhost;user=root;database=dotnet;password='Vahab@123'";
   
        public static List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            IDbConnection con = new MySqlConnection(connectionstring);
            try
            {
                con.Open();
                string query = "select * from customer";
                IDbCommand cmd = new MySqlCommand(query, con as MySqlConnection);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Customer cust = new Customer();
                    cust.cid = int.Parse(reader["cid"].ToString());
                    cust.cname = reader["cname"].ToString();
                    cust.contact = double.Parse(reader["contact"].ToString());
                    cust.location = reader["location"].ToString();
                    customers.Add(cust);
                }
                reader.Close();
            }
            catch(MySqlException e)
            {
                string error = e.Message;
            }
            finally
            {
                con.Close();
            }
            return customers;
        }
        
        public static BOL.Customer GetbyID(int id)
        {
            BOL.Customer cust = new BOL.Customer();
            IDbConnection con = new MySqlConnection(connectionstring);
            try
            {
                con.Open();
                string query="select * from customer where cid="+id;
                IDbCommand cmd = new MySqlCommand(query, con as MySqlConnection);
                cmd.CommandType = CommandType.Text;
                IDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    cust.cid = int.Parse(reader["cid"].ToString());
                    cust.cname = reader["cname"].ToString();
                    cust.location = reader["location"].ToString();
                    cust.contact = double.Parse(reader["contact"].ToString());
                    reader.Close();
                }
            }
            catch(MySqlException e)
            {
                string error = e.Message;
            }
            finally
            {
                con.Close();
            }
            return cust;
        }

        public static bool Insert(BOL.Customer cust)
        {
            bool flag = false;
            IDbConnection con = new MySqlConnection(connectionstring);
            try
            {
                con.Open();
                string query = "insert into customer values(" + cust.cid + ",'" + cust.cname + "','" + cust.location+ "'," + cust.contact+")";
                IDbCommand cmd = new MySqlCommand(query, con as MySqlConnection);
                cmd.CommandType = CommandType.Text;
                int dataadded= cmd.ExecuteNonQuery();
                if(dataadded>0)
                {
                    flag = true;
                }
            }
            catch(MySqlException e)
            {
                string msg=e.Message;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

        public static bool Delete(int id)
        {
            bool flag = true;
            IDbConnection con = new MySqlConnection(connectionstring);
            try
            {
                con.Open();
                string query = "delete from customer where cid=" + id;
                IDbCommand cmd = new MySqlCommand(query, con as MySqlConnection);
                cmd.CommandType = CommandType.Text;
                int deleteditem= cmd.ExecuteNonQuery();
                if(deleteditem>0)
                {
                    flag = false;
                }
            }
            catch(MySqlException ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

        public static bool Update(Customer cust)
        {
            bool flag = false;
            IDbConnection con = new MySqlConnection(connectionstring);
            try
            {
                con.Open();
                string query = "update customer set cid=" + cust.cid + ",cname='" + cust.cname +
                    "',location='" + cust.location + "',contact=" + cust.contact+" where cid="+cust.cid;
                IDbCommand cmd = new MySqlCommand(query, con as MySqlConnection);
                cmd.CommandType = CommandType.Text;
                int updated = cmd.ExecuteNonQuery();
                if(updated>0)
                {
                    flag = true;
                }
            }
            catch(MySqlException ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }
        public static bool InsertUsingParameter(Customer cust)//used for security purpose 
        {
            bool flag = false;
            IDbConnection con = new MySqlConnection(connectionstring);
            try
            {
                con.Open();
                string query = "INSERT INTO customer (cid,cname,location,contact)  values(@id, @Name, @location,@contact)";
                MySqlCommand cmd = new MySqlCommand(query, con as MySqlConnection);
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", cust.cid);
                cmd.Parameters.AddWithValue("@Name", cust.cname);
                cmd.Parameters.AddWithValue("@location", cust.location);
                cmd.Parameters.AddWithValue("@contact", cust.contact);
               int affected = cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    flag = true;
                }
            }
            catch(MySqlException ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

        public static bool CreateStoredProcedureandTable()
        {
            bool flag = false;
            MySqlConnection con = new MySqlConnection(connectionstring);
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection=con;

                con.Open();
                cmd.CommandText = "DROP PROCEDURE IF EXISTS add_people";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "DROP TABLE IF EXISTS people";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "CREATE TABLE people ( " +
                                  "id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY, first_name VARCHAR(20)," +
                                  "last_name VARCHAR(20), birthdate DATE)";
                cmd.ExecuteNonQuery();

                cmd.CommandText= "CREATE PROCEDURE add_people(" +
                                  "IN fname VARCHAR(20), IN lname VARCHAR(20), IN bday DATETIME, OUT id INT)" +
                                  "BEGIN INSERT INTO people(first_name, last_name, birthdate) " +
                                  "VALUES(fname, lname, DATE(bday)); SET id = LAST_INSERT_ID(); END";
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (MySqlException exp)
            {
                string msg = exp.Message;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return flag;
        }

        public static bool InvokeStoredProcedure()
        {
            bool status = false;
            MySqlConnection con = new MySqlConnection(connectionstring);
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "add_people";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fname", "Kamal");
                cmd.Parameters["@fname"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@lname", "Hasan");
                cmd.Parameters["@lname"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@bday", "1999-09-09");
                cmd.Parameters["@bday"].Direction = ParameterDirection.Input;

                cmd.Parameters.Add("@id", MySqlDbType.Int32);
                cmd.Parameters["@id"].Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                status = true;

            }
            catch (MySqlException exp)
            {
                string message = exp.Message;
            }
            finally
            {
                con.Close();
            }
            return status;
        }
    }
}