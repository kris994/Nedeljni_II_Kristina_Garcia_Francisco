namespace Nedeljni_II_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Current logged in super admin data
    /// </summary>
    public static class SuperAdmin
    {
        /// <summary>
        /// Super Admin Username
        /// </summary>
        private static string superAdminUsername;
        public static string SuperAdminUsername
        {
            get { return superAdminUsername; }
            set { superAdminUsername = value; }
        }

        /// <summary>
        /// Super Admin Password
        /// </summary>
        private static string superAdminPassword;
        public static string SuperAdminPassword
        {
            get { return superAdminPassword; }
            set { superAdminPassword = value; }
        }
    }
}
