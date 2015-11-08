using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess.DAO
{
    public abstract class AbstractDAO<T>
    {
        protected SqlConnection GetConnection()
	    {
	        return new SqlConnection(Properties.Resources.SqlConnectionString);
	    }

        public abstract void Create(T entity);
        public abstract T Read(int id);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);

        protected string Convert(object str)
        {
            return (str is DBNull) ? null : (string)str;
        }
    }
}
