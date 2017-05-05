using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Character_Creator
{
    class DBConnect
    {
        private MySqlConnection connection; //used to open a connection to a database
        private string server; //Indicates where our server is hosted (localhost)
        private string database; //the name of the database
        private string uid; //MySQL username
        private string password; //MySQL password
        private string tablename;

        //constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "rpchars";
            uid = "root";
            password = "19949542";
            tablename = "characters";
            string connectionString; //contains the connection string to connect to the database, and will be assigned to the connection variable
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server. Contact administrator");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //ExecuteNonQuery: Used to execute a command that will not return any data, for example Insert, update or delete.
        //ExecuteReader: Used to execute a command that will return 0 or more records, for example Select.
        //ExecuteScalar: Used to execute a command that will return only 1 value, for example Select Count(*).
        //Insert statement
        public void Insert(Character c)
        {
            string query = "INSERT INTO " + tablename + " (name, type, species, age, neighbourhood, appearance, personality, notes, S, P, E, C, I, A, L) VALUES('" + c.Name + "', '" + c.Type + "', '" + c.Species + "', '" + c.Age + "', '" + c.Neighbourhood + "', '" + c.Appearance + "', '" + c.Personality + "', '" + c.Notes + "', '" + c.S + "', '" + c.P + "', '" + c.E + "', '" + c.C + "', '" + c.I + "', '" + c.A + "', '" + c.L + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command and assign query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //Close connection
                this.CloseConnection();
            }

        }

        //Update statement
        public void Update(string UpdateQ)
        {
            string query = UpdateQ;

            //open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string DeleteQ)
        {
            string query = DeleteQ;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement makes an example list. modify this to adapt to a different database
        public List<string>[] Select(string name)
        {
            string query = "SELECT * FROM " + tablename + " WHERE name='" + name + "'";

            //Create a list to store the result
            List<string>[] list = new List<string>[16];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();
            list[7] = new List<string>();
            list[8] = new List<string>();
            list[9] = new List<string>();
            list[10] = new List<string>();
            list[11] = new List<string>();
            list[12] = new List<string>();
            list[13] = new List<string>();
            list[14] = new List<string>();
            list[15] = new List<string>();

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["type"] + "");
                    list[3].Add(dataReader["species"] + "");
                    list[4].Add(dataReader["age"] + "");
                    list[5].Add(dataReader["neighbourhood"] + "");
                    list[6].Add(dataReader["appearance"] + "");
                    list[7].Add(dataReader["personality"] + "");
                    list[8].Add(dataReader["notes"] + "");
                    list[9].Add(dataReader["S"] + "");
                    list[10].Add(dataReader["P"] + "");
                    list[11].Add(dataReader["E"] + "");
                    list[12].Add(dataReader["C"] + "");
                    list[13].Add(dataReader["I"] + "");
                    list[14].Add(dataReader["A"] + "");
                    list[15].Add(dataReader["L"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM " + tablename;
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create MySQL Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\MySqlBackup" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error, unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error, unable to Restore!");
            }
        }
    }
}
