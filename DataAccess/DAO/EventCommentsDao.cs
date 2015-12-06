using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Entities;

namespace DataAccess.DAO
{
    public class EventCommentsDao : AbstractDAO<Comment>
    {
        public override void Create(Comment entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Comment entity)
        {
            throw new NotImplementedException();
        }

        public override Comment Read(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Comment entity)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByEventId(int eventId) 
        {
            throw new NotImplementedException();
        }
    }
}
