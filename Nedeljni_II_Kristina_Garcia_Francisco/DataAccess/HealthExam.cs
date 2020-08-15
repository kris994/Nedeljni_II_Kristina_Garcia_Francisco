using Nedeljni_II_Kristina_Garcia_Francisco.Helper;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    class HealthExam
    {
        /// <summary>
        /// Symptoms file
        /// </summary>
        private readonly string file = @"~\..\..\..\TextFiles\AtRiskPatients.txt";

        /// <summary>
        /// Write about patients with symptoms to the file
        /// </summary>
        /// <param name="user">patient with symptoms</param>
        public void WriteToFile(tblUser user)
        {
            Validations val = new Validations();
            
            using (StreamWriter sw = new StreamWriter(file, append:true))
            {
                sw.WriteLine(user.FirstName + ":" + user.LastName + ":" + val.CalculateAge(user.DateOfBirth));
            }
        }

        /// <summary>
        /// Caluculates the average age of patiens
        /// </summary>
        /// <returns>average age</returns>
        public int AverageSickPatientsAge()
        {
            List<int> allAges = new List<int>();
            int totalAge = 0;

            // Load this only if the file exists
            if (File.Exists(file))
            {
                string[] readFile = File.ReadAllLines(file);

                for (int i = 0; i < readFile.Length; i++)
                {
                    if (!string.IsNullOrEmpty(readFile[i]))
                    {
                        string[] trim = readFile[i].Split(':');
                        int age = int.Parse(trim[2]);
                        allAges.Add(age);
                    }
                }
            }

            if (allAges.Count == 0)
            {
                return 0;
            }
            else
            {
                int totalPatients = allAges.Count;
                for (int i = 0; i < totalPatients; i++)
                {
                    totalAge = allAges[i] + totalAge;
                }

                return totalAge / totalPatients;
            }
        }
    }
}
