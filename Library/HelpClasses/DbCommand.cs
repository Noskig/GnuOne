using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.HelpClasses
{
    public class DbCommand
    {

        public static void CreateCommand(string queryString, string connectionstring)
        {
            try
            {
                using MySqlConnection conn = new MySqlConnection(connectionstring);
                {
                    MySqlCommand comm = conn.CreateCommand();
                    {
                        conn.Open();
                        comm.CommandText = queryString;
                        comm.ExecuteNonQuery();
                    }
                }
            }
              catch (Exception)
            {
                Console.WriteLine("Info: Db already exist/Or mail couldnt be read?");
                //Console.Error.WriteLine("Db kan inte skapas");
                //Console.Error.WriteLine(queryString);
                //Console.Error.WriteLine(connectionstring);
                //Console.WriteLine("Kontrollera queryn och connectionstring");
                return;
            }
        }
    }
}
