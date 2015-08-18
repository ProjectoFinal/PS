using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain.DataContract;

namespace SqlMapper.DataMapper
{
    public class CrimeTypeDataMapper : IDataMapper<CrimeType>
    {
        public static readonly string SQL_GET_ALL = "SELECT [id], [name]  From Crime_Type";
        public static readonly string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE id = @id";
        public static readonly string SQL_UPDATE = "UPDATE Crime_Type SET name = @name  WHERE id = @id";
        public static readonly string SQL_INSERT = "INSERT INTO Crime_Type (name) OUTPUT inserted.id values(@name) ";
        public static readonly string SQL_DELETE = "DELETE FROM Crime_Type WHERE id = @id";


        private readonly SqlConnection connection;
        private SqlTransaction trx;

        public CrimeTypeDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void SetTransaction(SqlTransaction transaction)
        {
            trx = transaction;
        }

        public CrimeType GetById(int id)
        {
            using (SqlCommand cmdGet = sqlGetById(connection))
            {
                cmdGet.Parameters["@id"].Value = id;
                using (SqlDataReader dr = cmdGet.ExecuteReader())
                {
                    dr.Read();
                    return Create(dr);
                }
            }
        }


        public CrimeType Create(SqlDataReader dr)
        {
            if (!dr.HasRows) return null;
            return new CrimeType((int) dr[0], (String) dr[1]);
        }

        public IEnumerable<CrimeType> GetAll( )
        {
            using (SqlCommand cmdGet = connection.CreateCommand())
            {
                cmdGet.CommandText = SQL_GET_ALL;


                using (SqlDataReader dr = cmdGet.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        yield return Create(dr);
                    }
                }
            }
        }

        public void Update(CrimeType p)
        {
            using (SqlCommand cmdUpdate = sqlUpdate(connection))
            {
                cmdUpdate.Parameters["@id"].Value = p.Id;
                cmdUpdate.Parameters["@name"].Value = p.Name;


                cmdUpdate.ExecuteNonQuery();
            }
        }

        public void Delete(CrimeType val)
        {
            using (SqlCommand cmd = sqlDelete(connection))
            {
                cmd.Parameters["@id"].Value = val.Id;
                cmd.ExecuteNonQuery();
            }
        }

        public int Insert(CrimeType val)
        {
            using (SqlCommand cmdInsert = sqlInsert(connection))
            {
                if (trx != null)
                    cmdInsert.Transaction = trx;

                cmdInsert.Parameters["@name"].Value = val.Name;
                cmdInsert.Parameters["@name"].Size = val.Name.Length;

                return val.Id = (int) cmdInsert.ExecuteScalar();
            }
        }

        public static SqlCommand sqlDelete(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_DELETE;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            return cmd;
        }

        public static SqlCommand sqlGetById(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_BY_ID;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            return cmd;
        }

        public static SqlCommand sqlUpdate(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_UPDATE;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            return cmd;
        }

        public static SqlCommand sqlInsert(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_INSERT;
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            return cmd;
        }
    }
}