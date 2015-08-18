using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain.DataContract;

namespace SqlMapper.DataMapper
{
    public class SessionDataMapper : IDataMapper<Session>
    {
        public static readonly string SQL_GET_ALL = "SELECT _user , token , emission_time , expiration_time  FROM [Session]";
        public static readonly string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE Session.id = @id";
        public static readonly string SQL_GET_BY_TOKEN = SQL_GET_ALL + " WHERE Session.token = @token";
        public static readonly string SQL_GET_BY_USER = SQL_GET_ALL + " WHERE Session._user = @user";

        public static readonly string SQL_UPDATE = "UPDATE Session SET name = @name, data = @data WHERE id = @id";


        public static readonly string SQL_INSERT =
            "INSERT INTO Session (_user , token , emission_time , expiration_time ) OUTPUT inserted.id VALUES ( @user , @token , @emission_time , @expiration_time) ";

        public static readonly string SQL_DELETE = "DELETE FROM Biometric_Type WHERE id = @id";
        private readonly SqlConnection connection;

        private SqlTransaction trx;

        public SessionDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void SetTransaction(SqlTransaction transaction)
        {
            trx = transaction;
        }


        public Session GetById(int id)
        {
            using (SqlCommand cmd = sqlGetById(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                cmd.Parameters["@id"].Value = id;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    return Create(dr);
                }
            }
        }

        public Session GetByToken ( String  token )
        {
            using (SqlCommand cmd = sqlGetByToken(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                cmd.Parameters["@token"].Value = token;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    Session session = Create(dr);
                    return session;
                }
            }
        }

        public Session GetByUser ( String user )
        {
            using (SqlCommand cmd = sqlGetByToken(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                cmd.Parameters["@user"].Value = user;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    Session session = Create(dr);
                    return session;
                }
            }
        }


        public Session Create(SqlDataReader dr)
        {
            var session = new Session();
            session.Id = (int) dr[0];
            return session;
        }

        public IEnumerable<Session> GetAll()
        {
            throw new NotSupportedException();
        }

        public void Update(Session val)
        {
            using (SqlCommand cmd = sqlUpdate(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                //cmd.Parameters["@name"].Value = val.Name;
                //cmd.Parameters["@data"].Value = val.data;
                //cmd.Parameters["@id"].Value = val.Id;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Session val)
        {
            using (SqlCommand cmd = sqlDelete(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                /*cmd.Parameters["@id"].Value = val.Id;*/
                cmd.ExecuteNonQuery();
            }
        }

        public int Insert(Session val)
        {
            using (SqlCommand cmd = sqlInsert(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;
                //cmd.Parameters["@name"].Size = val.Name.Length;
                //cmd.Parameters["@name"].Value = val.Name;

                //cmd.Parameters["@data"].Value = val.data;
                //cmd.Parameters["@person"].Value = val.Person.Id;

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

        public static SqlCommand sqlGetByUser(SqlConnection c)
        {

            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_BY_USER;
            cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.VarChar));
            return cmd;

        }

        public static SqlCommand sqlGetByToken(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_BY_TOKEN;
            cmd.Parameters.Add(new SqlParameter("@token", SqlDbType.VarChar));
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
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@data", SqlDbType.Image));
            return cmd;

        }

        public static SqlCommand sqlInsert(SqlConnection c)
        {

            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_INSERT;
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@person", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@data", SqlDbType.Image));
            return cmd;

        }
    }
}