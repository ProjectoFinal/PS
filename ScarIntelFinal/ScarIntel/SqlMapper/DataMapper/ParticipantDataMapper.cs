using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain.DataContract;

namespace SqlMapper.DataMapper
{
    public class ParticipantDataMapper : IDataMapper<Participant>
    {
        public static readonly string SQL_GET_ALL = "SELECT [id], [person] , [regist] FROM [Participant]";

        public static readonly string SQL_GET_NUMBER_PARTICIPANTS = "select count(*) from Participant  as p where p.regist=@idRegist";
        public static readonly string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE id = @id";
        public static readonly string SQL_GET_BY_REGIST = SQL_GET_ALL + " WHERE regist = @id";
        public static readonly string SQL_GET_BY_PERSON = SQL_GET_ALL + " WHERE person = @id";


        public static readonly string SQL_UPDATE = "UPDATE Participant SET number = @number WHERE id = @id";

        public static readonly string SQL_INSERT =
            "INSERT INTO Participant (person, regist) OUTPUT inserted.id VALUES (@person,@regist) ";

        public static readonly string SQL_DELETE = "DELETE FROM Participant WHERE id = @id";
        private readonly SqlConnection connection;
        private SqlTransaction trx;

        public ParticipantDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void SetTransaction(SqlTransaction transaction)
        {
            trx = transaction;
        }


        public Participant GetById(int id)
        {
            using (SqlCommand cmd = sqlGetById(connection))
            {
                cmd.Parameters["@id"].Value = id;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    return Create(dr);
                }
            }
        }

        
        public Participant Create(SqlDataReader dr)
        {
            var participant = new Participant();
            participant.Id = (int) dr[0];
            participant.Person.Id = (int) dr[1];
            participant.Regist.Id = (int) dr[2];
            return participant;
        }


        public IEnumerable<Participant> GetAll()
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = SQL_GET_ALL;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        yield return Create(dr);
                }
            }
        }

        public int GetCountParticipants(int registId)
        {
            using (SqlCommand cmd = sqlGetNumberOfPartic(connection))
            {
                cmd.Parameters["@idRegist"].Value = registId;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                   int count=0;
                   while (dr.Read()) count++;
                   return count;
                }
            }
        }
        public IEnumerable<Participant> GetAllByPerson( int person_id )
        {
            using (SqlCommand cmd = sqlGetById( SQL_GET_BY_PERSON , connection))
            {
                cmd.Parameters["@id"].Value = person_id;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        yield return Create(dr);
                }
            }
        }

        public IEnumerable<Participant> GetAllByRegist( int regist_id )
        {
            using (SqlCommand cmd = sqlGetById(SQL_GET_BY_REGIST, connection))
            {
                cmd.Parameters["@id"].Value = regist_id;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        yield return Create(dr);
                }
            }
        }

        public void Update(Participant val)
        {
            using (SqlCommand cmd = sqlUpdate(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;
                cmd.Parameters["@id"].Value = val.Id;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Participant val)
        {
            using (SqlCommand cmd = sqlDelete(connection))
            {
                cmd.Parameters["@id"].Value = val.Id;
                cmd.ExecuteNonQuery();
            }
        }

        public int Insert(Participant val)
        {
            using (SqlCommand cmd = sqlInsert(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                cmd.Parameters["@person"].Value = val.Person.Id;

                cmd.Parameters["@regist"].Value = val.Regist.Id;

                return val.Id = (int) cmd.ExecuteScalar();
            }
        }

        public static SqlCommand sqlGetById(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_BY_ID;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            return cmd;
        }

        public static SqlCommand sqlGetNumberOfPartic(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_NUMBER_PARTICIPANTS;
            cmd.Parameters.Add(new SqlParameter("@idRegist", SqlDbType.Int));
            return cmd;
        }
        public static SqlCommand sqlGetById(String sqlComandText , SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = sqlComandText;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            return cmd;
        }



        public static SqlCommand sqlDelete(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_DELETE;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            return cmd;
        }


        public static SqlCommand sqlUpdate(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_UPDATE;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            return cmd;
        }

        public static SqlCommand sqlInsert(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_INSERT;
            cmd.Parameters.Add(new SqlParameter("@person", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@regist", SqlDbType.Int));
            return cmd;
        }
    }
}