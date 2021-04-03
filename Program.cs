using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            CreateTable(sqlite_conn);
            InsertData(sqlite_conn);
            ReadData(sqlite_conn);
        }

        static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            string sqlString = "CREATE TABLE IF NOT EXISTS Customers(customer_id VARCHAR(20), name VARCHAR(20), surname VARCHAR(20), age INT)";
            sqlite_cmd.CommandText = sqlString;
            sqlite_cmd.ExecuteNonQuery();

            sqlString = "CREATE TABLE IF NOT EXISTS Transactions(transaction_id VARCHAR(20), customer_id VARCHAR(20), product_id VARCHAR(20), date TEXT, amount REAL)";
            sqlite_cmd.CommandText = sqlString;
            sqlite_cmd.ExecuteNonQuery();

            sqlString = "CREATE TABLE IF NOT EXISTS Products(product_id VARCHAR(20), name VARCHAR(20), description VARCHAR(20))";
            sqlite_cmd.CommandText = sqlString;
            sqlite_cmd.ExecuteNonQuery();

            Console.WriteLine("Tables created");
        }

        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            string sqlString = "INSERT INTO Customers(customer_id, name, surname, age) VALUES('cust1', 'Jenifer', 'Lopez', 38); ";
            sqlite_cmd.CommandText = sqlString;
            sqlite_cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO Customers(customer_id, name, surname, age) VALUES('cust2', 'Anna', 'Anders', 25); ";
            sqlite_cmd.CommandText = sqlString;
            sqlite_cmd.ExecuteNonQuery();

            sqlString = "INSERT INTO Customers(customer_id, name, surname, age) VALUES('cust3', 'John', 'Nash', 57); ";
            sqlite_cmd.CommandText = sqlString;
            sqlite_cmd.ExecuteNonQuery();

            Console.WriteLine("Data inserted");
        }

        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();

            string sqlString = "SELECT * FROM Customers";

            sqlite_cmd.CommandText = sqlString;

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = $"{sqlite_datareader.GetString(0)} {sqlite_datareader.GetString(1)} " +
                                    $"{sqlite_datareader.GetString(2)}";
                Console.WriteLine(myreader);
            }
            conn.Close();
        }
    }
}
