using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Entities;

namespace DataAccess.DAO
{
    public class EventDAO : AbstractDAO<Event>
    {
        public override void Create(Event entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "INSERT INTO PmiDatabase.dbo.events(Name, Location, Description, Image_id, Organizer_id, Event_time)" +
                                   "VALUES(@Name, @Location, @Description, @Image_id, @Organizer_id, @Event_time)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Name", SqlDbType.Text).Value = entity.Name;
                cmd.Parameters.Add("@Location", SqlDbType.VarChar, 200).Value = entity.Location;
                cmd.Parameters.Add("@Description", SqlDbType.Text).Value = entity.Decription;
                cmd.Parameters.Add("@Image_id", SqlDbType.Int).Value = entity.ImageId;
                cmd.Parameters.Add("@Organizer_id", SqlDbType.Int).Value = entity.OrganizerId;
                cmd.Parameters.Add("@Event_time", SqlDbType.DateTime).Value = entity.Time;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override Event Read(int id)
        {
            Event ev;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM PmiDatabase.dbo.events WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    ev = new Event
                    {
                        Id = (int)reader["id"],
                        Name = Convert(reader["Name"]),
                        Location = Convert(reader["Location"]),
                        Decription = Convert(reader["Description"]),
                        ImageId = (int)reader["Image_id"],
                        OrganizerId = (int)reader["Organizer_id"],
                        Time = (reader["Event_time"] is DBNull) ? DateTime.Now : (DateTime)reader["Event_time"]
                    };
                }
            }
            return ev;
        }

        public override void Update(Event entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "UPDATE PmiDatabase.dbo.events SET Name = @Name, Location = @Location, Description = @Description,"
                                   + " Image_id = @Image_id, Organizer_id = @Organizer_id, Event_time = @Event_time WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Name", SqlDbType.Text).Value = entity.Name;
                cmd.Parameters.Add("@Location", SqlDbType.VarChar, 200).Value = entity.Location;
                cmd.Parameters.Add("@Description", SqlDbType.Text).Value = entity.Decription;
                cmd.Parameters.Add("@Image_id", SqlDbType.Int).Value = entity.ImageId;
                cmd.Parameters.Add("@Organizer_id", SqlDbType.Int).Value = entity.OrganizerId;
                cmd.Parameters.Add("@Event_time", SqlDbType.DateTime).Value = entity.Time;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(Event entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "DELETE FROM PmiDatabase.dbo.events WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@ID", SqlDbType.VarChar, 255).Value = entity.Id;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override List<Event> ReadAll()
        {
            var result = new List<Event>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string commandString = "SELECT * FROM PmiDatabase.dbo.events";
                SqlCommand cmd = new SqlCommand(commandString, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Event
                        {
                            Id = (int)reader["id"],
                            Name = Convert(reader["Name"]),
                            Location = Convert(reader["Location"]),
                            Decription = Convert(reader["Description"]),
                            ImageId = (int)reader["Image_id"],
                            OrganizerId = (int)reader["Organizer_id"],
                            Time = (reader["Event_time"] is DBNull) ? DateTime.Now : (DateTime)reader["Event_time"]
                        });
                    }
                }
            }
            return result;
        }

        public List<Event> ReadEventsCreatedByUser(int userId)
        {
            var list = new List<Event>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                const string sql = "SELECT * FROM PmiDatabase.dbo.events WHERE Organizer_id = @Organizer_id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Organizer_id", SqlDbType.Int).Value = userId;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Event ev = new Event
                        {
                            Id = (int)reader["id"],
                            Name = Convert(reader["Name"]),
                            Location = Convert(reader["Location"]),
                            Decription = Convert(reader["Description"]),
                            ImageId = (int)reader["Image_id"],
                            OrganizerId = (int)reader["Organizer_id"],
                            Time = (reader["Event_time"] is DBNull) ? DateTime.Now : (DateTime)reader["Event_time"]
                        };
                        list.Add(ev);
                    }
                }
            }
            return list;
        }
    }
}
