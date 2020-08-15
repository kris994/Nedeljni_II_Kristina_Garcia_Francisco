namespace Nedeljni_II_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Current logged in user data
    /// </summary>
    public static class LoggedInUser
    {
        private static tblUser currentUser;
        /// <summary>
        /// Current user
        /// </summary>
        public static tblUser CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
    }
}
