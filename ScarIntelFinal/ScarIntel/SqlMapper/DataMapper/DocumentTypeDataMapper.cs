using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain.DataContract;

namespace SqlMapper.DataMapper
{
    public class DocumentTypeDataMapper : IDataMapper<DocumentType>
    {
        public static readonly string SQL_GET_ALL = "SELECT [id], [name]  From Document_Type";
        public static readonly string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE id = @id";
        public static readonly string SQL_UPDATE = "UPDATE Document_Type SET name = @name  WHERE id = @id";
        public static readonly string SQL_INSERT = "INSERT INTO Document_Type (name)  OUTPUT inserted.id values(@name) ";
        public static readonly string SQL_DELETE = "DELETE FROM Document WHERE id = @id";


        private readonly SqlConnection connection;
        private SqlTransaction trx;

        public DocumentTypeDataMapper(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public void SetTransaction(SqlTransaction transaction)
        {
            trx = transaction;
        }


        public DocumentType Create(SqlDataReader dr)
        {
            if (!dr.HasRows) return null;
            var type = new DocumentType(
                (int) dr[0],
                (String) dr[1]);
            return type;
        }

        public DocumentType GetById(int id)
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


        public IEnumerable<DocumentType> GetAll()
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

        public void Update(DocumentType p)
        {
            using (SqlCommand cmd = sqlUpdate(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;
                cmd.Parameters["@id"].Value = p.Id;
                cmd.Parameters["@name"].Value = p.Name;
                cmd.ExecuteNonQuery();
            }
        }


        public void Delete(DocumentType val)
        {
            using (SqlCommand cmd = sqlDelete(connection))
            {
                cmd.Parameters["@id"].Value = val.Id;
                cmd.ExecuteNonQuery();
            }
        }

        public int Insert(DocumentType val)
        {
            using (SqlCommand cmd = sqlInsert(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;
                cmd.Parameters["@name"].Value = val.Name;
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