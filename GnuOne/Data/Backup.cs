using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuOne.Data
{
    public class Backup
    {
        private static string constring = "server=localhost;user=root;pwd=Hejsan123!;database=gnu;";
        private static string file = @"\\SQL\backup.sql";
        private static string path = Directory.GetCurrentDirectory();
        private static string fullpath = Path.GetFullPath(path + file);

        public static void BackupDatabase()
        {

            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(fullpath);
                        conn.Close();
                    }
                }
            }
        }

        public static void GetBackUp()
        {
            using (MySqlConnection conn = new MySqlConnection(constring))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ImportFromFile(fullpath);
                        conn.Close();
                    }
                }
            }
        }

    }
}