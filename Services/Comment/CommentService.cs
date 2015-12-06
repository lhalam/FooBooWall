using DataAccess.Entities;
using DataAccess.DAO;
using Services.DTO;
using System.Collections.Generic;

namespace Services
{
    public class CommentService : ICommentService
    {
        private EventCommentsDao _commentsDao;

        public CommentService(EventCommentsDao commentsDao) 
        {
            _commentsDao = commentsDao;
        }

        private Comment Get(int id)
        {
            return _commentsDao.Read(id);
        }

        public List<Comment> GetEventComments(int eventId)
        {
            return _commentsDao.GetCommentsByEventId(eventId);
        }

        public void Edit(CommentDTO commentDTO)
        {
            Comment c = Get(commentDTO.Id);

            c.Text = commentDTO.Text;

            _commentsDao.Update(c);
        }

        public void Delete(int commentId)
        {
            Comment c = Get(commentId);

            _commentsDao.Delete(c);
        }

        public void Create(Comment comment) 
        {
            _commentsDao.Create(comment);
        }
    }
}
