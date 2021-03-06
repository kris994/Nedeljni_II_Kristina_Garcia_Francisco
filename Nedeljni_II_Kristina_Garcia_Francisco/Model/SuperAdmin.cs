﻿namespace Nedeljni_II_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Current logged in super admin data
    /// </summary>
    public static class SuperAdmin
    {
        private static string superAdminUsername;
        /// <summary>
        /// Super Admin Username
        /// </summary>
        public static string SuperAdminUsername
        {
            get { return superAdminUsername; }
            set { superAdminUsername = value; }
        }

        private static string superAdminPassword;
        /// <summary>
        /// Super Admin Password
        /// </summary>
        public static string SuperAdminPassword
        {
            get { return superAdminPassword; }
            set { superAdminPassword = value; }
        }
    }
}
