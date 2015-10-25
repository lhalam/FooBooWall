﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess.DAO
{
    public class EventDAO: AbstractDAO<Event>
    {
        public override void Create(Event entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "INSERT INTO PmiDatabase.dbo.events(Name, Location, Description, Image_id, Organizer_id, Event_time)" +
                    "VALUES(@param1, @param2, @param3, @param4, @param5, @param6)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Text).Value = entity.Name;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 200).Value = entity.Location;
                cmd.Parameters.Add("@param3", SqlDbType.Text).Value = entity.Decription;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = entity.Image_ID;
                cmd.Parameters.Add("@param5", SqlDbType.Int).Value = entity.Organizer_id;
                cmd.Parameters.Add("@param6", SqlDbType.DateTime).Value = entity.Time;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override Event Read(int id)
        {
            Event ev = null;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM PmiDatabase.dbo.events WHERE id = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    ev = new Event
                    {
                        ID = (int)reader.GetValue(0),
                        Name = (string)reader.GetValue(1),
                        Location = (string)reader.GetValue(2),
                        Decription = (string)reader.GetValue(3),
                        Image_ID = (int)reader.GetValue(4),
                        Organizer_id = (int)reader.GetValue(5),
                        Time = (DateTime)reader.GetValue(6),                       
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
                string sql = "UPDATE PmiDatabase.dbo.events SET Name = @param1, Location = @param2, Description = @param3,"
                + " Image_id = @param4, Organizer_id = @param5, Event_time = @param6 WHERE ID = @param7";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.Text).Value = entity.Name;
                cmd.Parameters.Add("@param2", SqlDbType.VarChar, 200).Value = entity.Location;
                cmd.Parameters.Add("@param3", SqlDbType.Text).Value = entity.Decription;
                cmd.Parameters.Add("@param4", SqlDbType.Int).Value = entity.Image_ID;
                cmd.Parameters.Add("@param5", SqlDbType.Int).Value = entity.Organizer_id;
                cmd.Parameters.Add("@param6", SqlDbType.DateTime).Value = entity.Time;
                cmd.Parameters.Add("@param7", SqlDbType.Int).Value = entity.ID;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public override void Delete(Event entity)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "DELETE FROM PmiDatabase.dbo.events WHERE ID = @param1";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).Value = entity.ID;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
