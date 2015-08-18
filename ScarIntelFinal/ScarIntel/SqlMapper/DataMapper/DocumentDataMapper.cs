using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain.DataContract;

namespace SqlMapper.DataMapper
{
    public class DocumentDataMapper : IDataMapper<Document>
    {
        public static readonly string SQL_GET_ALL =
            "SELECT [id], [code] ,[emission_local],  [emission_date], [expiration_date],[docu_type],[person_id] From Document";

        public static readonly string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE id = @id";

        public static readonly string SQL_UPDATE =
            "UPDATE Document SET code = @code,  emission_local = @emission_local , emission_date=@emission_date,expiration_date=@expiration_date WHERE id = @id";

        public static readonly string SQL_INSERT =
            "INSERT INTO Document (code, emission_local, emission_date, expiration_date,docu_type,person_id)  OUTPUT inserted.id values (@code,@emission_local,@emission_date,@expiration_date,@docu_type,@person_id)  ";

        public static readonly string SQL_DELETE = "DELETE FROM Document WHERE id = @id";


        private readonly SqlConnection connection;
        private SqlTransaction trx;

        public DocumentDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void SetTransaction(SqlTransaction transaction)
        {
            trx = transaction;
        }


        public Document GetById(int id)
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


        // [id], [code] ,[emission_local],  [emission_date], [expiration_date],[docu_type],[person_id] From Document";
        public Document Create(SqlDataReader dr)
        {
            var document = new Document();
            document.id = (int) dr[0];
            document.code = (string) dr[1];
            document.emission_local = (string) dr[2];
            document.emission_date = (DateTime) dr[3];
            document.expiration_date = (DateTime) dr[4];
            document.Type.Id = (int) dr[5];
            document.Person.Id = (int) dr[6];
            return document;
        }


        public IEnumerable<Document> GetAll()
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

        public void Update(Document p)
        {
            using (SqlCommand cmdUpdate = sqlUpdate(connection))
            {
                cmdUpdate.Parameters["@id"].Value = p.id;
                cmdUpdate.Parameters["@code"].Value = p.code;
                cmdUpdate.Parameters["@emission_local"].Value = p.emission_local;
                cmdUpdate.Parameters["@emission_date"].Value = p.emission_date;
                cmdUpdate.Parameters["@expiration_date"].Value = p.expiration_date;

                cmdUpdate.ExecuteNonQuery();
            }
        }


        public void Delete(Document val)
        {
            using (SqlCommand cmd = sqlDelete(connection))
            {
                cmd.Parameters["@id"].Value = val.id;
                cmd.ExecuteNonQuery();
            }
        }

        public int Insert(Document val)
        {
            using (SqlCommand cmdInsert = sqlInsert(connection))
            {
                if (trx != null)
                    cmdInsert.Transaction = trx;

                cmdInsert.Parameters["@code"].Value = val.code;
                cmdInsert.Parameters["@emission_local"].Value = val.emission_local;
                cmdInsert.Parameters["@emission_date"].Value = val.emission_date;
                cmdInsert.Parameters["@expiration_date"].Value = val.expiration_date;

                cmdInsert.Parameters["@docu_type"].Value = val.Type.Id;

                cmdInsert.Parameters["@person_id"].Value = val.Person.Id;

                return val.id = (int) cmdInsert.ExecuteScalar();
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
            cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@emission_local", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@emission_date", SqlDbType.DateTime));
            cmd.Parameters.Add(new SqlParameter("@expiration_date", SqlDbType.DateTime));
            return cmd;
        }


        public static SqlCommand sqlInsert(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_INSERT;
            cmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@emission_local", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@emission_date", SqlDbType.DateTime));
            cmd.Parameters.Add(new SqlParameter("@expiration_date", SqlDbType.DateTime));
            cmd.Parameters.Add(new SqlParameter("@docu_type", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@person_id", SqlDbType.Int));


            return cmd;
        }
    }
}