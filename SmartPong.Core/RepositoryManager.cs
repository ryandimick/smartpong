namespace SmartPong
{
    /// <summary>
    /// 
    /// Entry point for generating a repository for interacting with a database.
    /// 
    /// </summary>
    public class RepositoryManager
    {
        /// <summary>
        /// 
        /// Generates a new SmartPong Repository and returns it.
        /// 
        /// </summary>
        /// 
        /// <param name="connectionString">The connection information to access the database.</param>
        /// 
        /// <returns>A SmartPong repository.</returns>
        public static ISmartPongRepository Create(string connectionString)
        {
            return new SmartPongRepository(connectionString);
        }
    }
}
