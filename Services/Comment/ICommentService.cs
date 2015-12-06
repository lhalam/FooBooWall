using System;
using System.Collections.Generic;
using DataAccess.Entities;
using Services.DTO;
namespace Services
{
    interface ICommentService
    {
        void Create(Comment comment);

        void Delete(int commentId);

        void Edit(CommentDTO commentDTO);

        List<Comment> GetEventComments(int eventId);
    }
}
