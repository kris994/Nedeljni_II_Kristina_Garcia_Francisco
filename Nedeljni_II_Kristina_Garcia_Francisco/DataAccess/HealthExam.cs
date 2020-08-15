using Nedeljni_II_Kristina_Garcia_Francisco.Helper;
using Nedeljni_II_Kristina_Garcia_Francisco.Model;
using System;
using System.IO;

namespace Nedeljni_II_Kristina_Garcia_Francisco.DataAccess
{
    class HealthExam
    {
        /// <summary>
        /// Symptoms file
        /// </summary>
        private readonly string file = @"~\..\..\..\TextFiles\AtRiskPatients.txt";

        public void WriteToFile(tblUser user)
        {
            Validations val = new Validations();
            
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine(user.FirstName + ":" + user.LastName + ":" + val.CalculateAge(user.DateOfBirth));
            }
        }
    }
}
