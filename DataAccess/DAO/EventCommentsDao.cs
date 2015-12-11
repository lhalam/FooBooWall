using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Entities;
using System.Data.SqlTypes;

namespace DataAccess.DAO
{
    public class EventCommentsDao : AbstractDAO<Comment>
    {
        public override void Create(Comment entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "INSERT INTO comments(Event_id, Author_id, Comment_time, Comment)" +
                    " OUTPUT Inserted.ID " +
                    "VALUES(@Event_id, @Author_id, @Comment_time, @Comment)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Event_id", SqlDbType.Int).Value = entity.EventId;
                cmd.Parameters.Add("@Author_id", SqlDbType.Int).Value = entity.AuthorId;
                cmd.Parameters.Add("@Comment_time", SqlDbType.Date).Value = entity.WritingDate;
                cmd.Parameters.Add("@Comment", SqlDbType.Text).Value = entity.Text ?? SqlString.Null;
                cmd.Parameters.Add("@param5", SqlDbType.Int).Value = entity.Id; // TODO: try to comment this and see if anything changes
                cmd.CommandType = CommandType.Text;
                entity.Id = (int)cmd.ExecuteScalar();
            }
        }

        public override void Delete(Comment entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM comments WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override Comment Read(int id)
        {
            Comment comment = null;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM comments WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    comment = new Comment
                    {
                        Id = id,
                        EventId = (int)reader["Event_id"],
                        AuthorId = (int)reader["Author_id"],
                        WritingDate = (reader["Comment_time"] is DBNull) ? DateTime.Now : (DateTime)reader["Comment_time"]
                    };
                }
            }
            return comment;
        }

        public override void Update(Comment entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "UPDATE comments SET Event_id = @Event_id, Author_id = @Author_id, Comment_time = @Comment_time," +
                    " Comment = @Comment WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Event_id", SqlDbType.Int).Value = entity.EventId;
                cmd.Parameters.Add("@Author_id", SqlDbType.Int).Value = entity.AuthorId;
                cmd.Parameters.Add("@Comment_time", SqlDbType.Date).Value = entity.WritingDate;
                cmd.Parameters.Add("@Comment", SqlDbType.Text).Value = entity.Text ?? SqlString.Null;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public List<Comment> GetCommentsByEventId(int eventId) 
        {
            var list = new List<Comment>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT c.Id AS Id, c.Event_id AS Event_id, c.Author_id AS Author_id, " +
                    "c.Comment_time AS Comment_time, u.Name AS AuthorName, i.Name AS ImageName FROM comments c INNER JOIN users u ON c.Author_id = u.ID " +
                 " INNER JOIN Events e ON c.Event_id = e.ID INNER JOIN Images i ON e.Image_id = i.ID";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = (int)reader["Id"],
                            EventId = (int)reader["Event_id"],
                            AuthorId = (int)reader["Author_id"],
                            WritingDate = (reader["Comment_time"] is DBNull) ? DateTime.Now : (DateTime)reader["Comment_time"],
                            AuthorName = Convert(reader["AuthorName"]),
                            ImageName = Convert(reader["ImageName"])
                        };
                        list.Add(comment);
                    }
                }
            }
            return list;
        }
    }
}
