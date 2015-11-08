using System.Data.SqlClient;
using DataAccess.Properties;

namespace DataAccess.DAO
{
    public abstract class AbstractDAO<T>
    {
        protected SqlConnection GetConnection()
	    {
	        return new SqlConnection(Resources.SqlConnectionString);
	    }

        public abstract void Create(T entity);
        public abstract T Read(int id);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
    }
}
