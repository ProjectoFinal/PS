using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain.DataContract;

namespace SqlMapper.DataMapper
{
    public class RegistDataMapper : IDataMapper<Regist>
    {
        public static readonly string SQL_GET_ALL = "SELECT [id],[data] ,[descript] FROM [Regist]";

        public static readonly string SQL_GET_REGIST_BY_PERSON = "select r.id,r.data,r.descript from Participant as p join Regist as r on(r.id=p.regist) where p.person=@idPerson";
       
        public static readonly string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE id = @id";

        public static readonly string SQL_GET_CRIMETYPE = "select ct.id,ct.name from Regist as r join Regist_CrimeType as rct on(r.id=rct.regist) join Crime_Type as ct on(ct.id=rct.crimeType) where r.id=@idRegist";
        

        public static readonly string SQL_UPDATE =
            "UPDATE Regist SET descript = @descript,  data = @data WHERE id = @id";

        public static readonly string SQL_INSERT =
            "INSERT INTO Regist (data,descript) OUTPUT inserted.id VALUES ( @data,@descript) ";

        public static readonly string SQL_DELETE = "DELETE FROM Regist WHERE id = @id";

        private readonly SqlConnection connection;
        private SqlTransaction trx;

        public RegistDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void SetTransaction(SqlTransaction transaction)
        {
            trx = transaction;
        }


        public Regist GetById(int id)
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


        public IEnumerable<Regist> GetRegistsByPerson(int idPerson)
        {
            using (SqlCommand cmd = sqlGetRegistByPersonId(connection))
            {
                cmd.Parameters["@idPerson"].Value = idPerson;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    yield return Create(dr);
                }
            }
        }
        

        public Regist Create(SqlDataReader dr)
        {
            var regist = new Regist();
            regist.Id = (int) dr[0];
            regist.Description = (string) dr[2];
            regist.Date = (DateTime) dr[1];
            return regist;
        }

        public IEnumerable<Regist> GetAll()
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

        public IEnumerable<CrimeType> GetCrimeType(int idRegist)
        {
            using (SqlCommand cmd = sqlGetCrimeType(connection))
            {
                
                cmd.Parameters["@idRegist"].Value = idRegist;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        yield return new CrimeType((int)dr[0],(String)dr[1]);
                    
                }
            }
        }


        public void Update(Regist val)
        {
            using (SqlCommand cmd = sqlUpdate(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                cmd.Parameters["@descript"].Size = val.Description.Length;
                cmd.Parameters["@descript"].Value = val.Description;
                cmd.Parameters["@id"].Value = val.Id;

                cmd.Parameters["@data"].Value = val.Date;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Regist val)
        {
            using (SqlCommand cmd = sqlDelete(connection))
            {
                cmd.Parameters["@id"].Value = val.Id;
                cmd.ExecuteNonQuery();
            }
        }

        public int Insert(Regist val)
        {
            using (SqlCommand cmd = sqlInsert(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                cmd.Parameters["@descript"].Size = val.Description.Length;
                cmd.Parameters["@descript"].Value = val.Description;

                cmd.Parameters["@data"].Value = val.Date;

        

                return val.Id = (int) cmd.ExecuteScalar();
            }
        }
        

           public static SqlCommand sqlGetCrimeType(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_CRIMETYPE;
            cmd.Parameters.Add(new SqlParameter("@idRegist", SqlDbType.Int));
            return cmd;
        }
        public static SqlCommand sqlGetById(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_BY_ID;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            return cmd;
        }
        public static SqlCommand sqlGetRegistByPersonId(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_REGIST_BY_PERSON;
            cmd.Parameters.Add(new SqlParameter("@idPerson", SqlDbType.Int));
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
            cmd.Parameters.Add(new SqlParameter("@descript", SqlDbType.VarChar));

            cmd.Parameters.Add(new SqlParameter("@data", SqlDbType.DateTime));
            return cmd;
        }

        public static SqlCommand sqlInsert(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_INSERT;
            cmd.Parameters.Add(new SqlParameter("@descript", SqlDbType.VarChar));

            cmd.Parameters.Add(new SqlParameter("@data", SqlDbType.Date));
            return cmd;
        }
    }
}