using System.IO;

namespace Nedeljni_II_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Reads or Writes from the file
    /// </summary>
    class FileReadWrite
    {
        /// <summary>
        /// Super Admin access file
        /// </summary>
        private readonly string file = @"~\..\..\..\TextFiles\ClinicAccess.txt";

        /// <summary>
        /// Read super admin from the file and save it
        /// </summary>
        public void ReadAdminFile()
        {
            // Load this only if the file exists
            if (File.Exists(file))
            {
                string[] readFile = File.ReadAllLines(file);

                for (int i = 0; i < readFile.Length; i++)
                {
                    if (!string.IsNullOrEmpty(readFile[i]))
                    {
                        string[] trim = readFile[i].Split(':');
                        SuperAdmin.SuperAdminUsername = trim[0];
                        SuperAdmin.SuperAdminPassword = trim[1];
                    }
                }
            }
        }

        /// <summary>
        /// Writes to the super admin file
        /// </summary>
        /// <param name="username">Super Admin username that is being added</param>
        /// <param name="password">Super Admin password</param>
        public void WriteAdminFile(string username, string password)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine(username + ":" + password);
            }
        }
    }
}
