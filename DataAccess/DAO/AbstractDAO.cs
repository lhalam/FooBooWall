using System;
using System.Collections.Generic;
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

        public virtual List<T> ReadAll()
        {
            return new List<T>();
        }

        public abstract void Update(T entity);
        public abstract void Delete(T entity);

        protected string Convert(object str)
        {
            return (str is DBNull) ? null : (string)str;
        }
    }
}
