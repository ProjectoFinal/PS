using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain.DataContract;

namespace SqlMapper.DataMapper
{
    public class BiometricTypeDataMapper : IDataMapper<BiometricType>
    {
        public static readonly string SQL_GET_ALL = "SELECT [id], [name], [person] , [data] FROM [Biometric_Type]";
        public static readonly string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE Biometric_Type.id = @id";
        public static readonly string SQL_UPDATE = "UPDATE Biometric_Type SET name = @name, data = @data WHERE id = @id";

        public static readonly string SQL_INSERT =
            "INSERT INTO Biometric_Type (name, data, person) OUTPUT inserted.id VALUES (@name, @data, @person) ";

        public static readonly string SQL_DELETE = "DELETE FROM Biometric_Type WHERE id = @id";
        private readonly SqlConnection connection;

        private SqlTransaction trx;

        public BiometricTypeDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void SetTransaction(SqlTransaction transaction)
        {
            trx = transaction;
        }


        public BiometricType GetById(int id)
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


        public BiometricType Create(SqlDataReader dr)
        {
            var bio = new BiometricType();
            bio.Id = (int) dr[0];
            bio.Name = (string) dr[1];
            bio.Person.Id = (int) dr[2];
            bio.data = (byte[]) dr[3];
            return bio;
        }

        public IEnumerable<BiometricType> GetAll()
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

        public void Update(BiometricType val)
        {
            using (SqlCommand cmd = sqlUpdate(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                cmd.Parameters["@name"].Value = val.Name;

                cmd.Parameters["@data"].Value = val.data;

                cmd.Parameters["@id"].Value = val.Id;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(BiometricType val)
        {
            using (SqlCommand cmd = sqlDelete(connection))
            {
                cmd.Parameters["@id"].Value = val.Id;
                cmd.ExecuteNonQuery();
            }
        }

        public int Insert(BiometricType val)
        {
            using (SqlCommand cmd = sqlInsert(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;
                cmd.Parameters["@name"].Size = val.Name.Length;
                cmd.Parameters["@name"].Value = val.Name;

                cmd.Parameters["@data"].Value = val.data;
                cmd.Parameters["@person"].Value = val.Person.Id;

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