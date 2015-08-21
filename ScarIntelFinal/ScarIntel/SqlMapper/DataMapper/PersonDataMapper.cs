using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Domain.DataContract;

namespace SqlMapper.DataMapper
{
    public class PersonDataMapper : IDataMapper<Person>
    {
        public static readonly string SQL_GET_ALL =
            "SELECT [id], [name], [birthday], [birthplace], [adress] FROM [Person]";

        public static readonly string SQL_GET_BY_ID = SQL_GET_ALL + " WHERE Person.id = @id";
        public static readonly string SQL_GET_BY_ID_REGIST = "select Person.id,Person.name,Person.birthday,Person.birthplace,Person.adress from Participant join Person on(Person.id=Participant.person) where Participant.regist=@idRegist";

        public static readonly string SQL_UPDATE =
            "UPDATE Person SET name = @name, birthday = @birthday, birthplace = @birthplace, adress = @adress WHERE id = @id";

        public static readonly string SQL_INSERT =
            "INSERT INTO Person ( name, birthday, birthplace, adress) OUTPUT inserted.id VALUES (@name, @birthday, @birthplace, @adress) ";

        public static readonly string SQL_DELETE = "DELETE FROM PERSON WHERE Person.id = @id";
        private readonly SqlConnection connection;
        private SqlTransaction trx;

        public PersonDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void SetTransaction(SqlTransaction transaction)
        {
            trx = transaction;
        }

        public IEnumerable<Person> GetByFilters(PersonSearchParams filters)
        {

            using (SqlCommand cmd = this.connection.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = paging
                //cmdGet.Parameters["@id"].Value = id;
                using (SqlDataReader dr = cmdGet.ExecuteReader())
                {
                    
                    while (dr.Read())
                        yield return Create(dr);
                }
            }
        }


        public Person GetById(int id)
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


        public IEnumerable<Person> GetAllPersonByRegist(int idRegist)
        {
            using (SqlCommand cmdGet = sqlGetByRegist(connection))
            {
                cmdGet.Parameters["@idRegist"].Value = idRegist;
                using (SqlDataReader dr = cmdGet.ExecuteReader())
                {
                    while (dr.Read())
                        yield return Create(dr);
                }
            }
        }

        public Person Create(SqlDataReader dr)
        {
            if (!dr.HasRows) return null;

            var id = (int) dr[0];
            var name = (string) dr[1];
            var date = (DateTime) dr[2];
            var naturalidade = (string) dr[3];
            var morada = (string) dr[4];

            return new Person(id, name, date, naturalidade, morada);
        }

        public IEnumerable<Person> GetAll()
        {
            using (SqlCommand cmdGet = connection.CreateCommand())
            {
                cmdGet.CommandText = SQL_GET_ALL;

                using (SqlDataReader dr = cmdGet.ExecuteReader())
                {
                    while (dr.Read())
                        yield return Create(dr);
                }
            }
        }

        public void Update(Person p)
        {
            using (SqlCommand cmd = sqlUpdate(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                cmd.Parameters["@name"].Size = p.Name.Length;
                cmd.Parameters["@name"].Value = p.Name;

                cmd.Parameters["@id"].Value = p.Id;

                cmd.Parameters["@birthday"].Value = p.Birthday;

                cmd.Parameters["@birthplace"].Size = p.Birthplace.Length;
                cmd.Parameters["@birthplace"].Value = p.Birthplace;

                cmd.Parameters["@adress"].Size = p.Address.Length;
                cmd.Parameters["@adress"].Value = p.Address;

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Person p)
        {
            using (SqlCommand cmd = sqlDelete(connection))
            {
                cmd.Parameters["@id"].Value = p.Id;
                cmd.ExecuteNonQuery();
            }
        }

        public int Insert(Person p)
        {
            using (SqlCommand cmd = sqlInsert(connection))
            {
                if (trx != null)
                    cmd.Transaction = trx;

                cmd.Parameters["@name"].Size = p.Name.Length;
                cmd.Parameters["@name"].Value = p.Name;

                cmd.Parameters["@birthday"].Value = p.Birthday;

                cmd.Parameters["@birthplace"].Size = p.Birthplace.Length;
                cmd.Parameters["@birthplace"].Value = p.Birthplace;

                cmd.Parameters["@adress"].Size = p.Address.Length;
                cmd.Parameters["@adress"].Value = p.Address;

                return p.Id = (int) cmd.ExecuteScalar();
            }
        }

        public static SqlCommand sqlGetById(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_BY_ID;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            return cmd;
        }

        public static SqlCommand sqlGetByFilter(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_BY_ID;
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
            return cmd;
        }

        public static SqlCommand sqlGetByRegist(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_GET_BY_ID_REGIST;
            cmd.Parameters.Add(new SqlParameter("@idRegist", SqlDbType.Int));
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
            cmd.Parameters.Add(new SqlParameter("@birthday", SqlDbType.DateTime));
            cmd.Parameters.Add(new SqlParameter("@birthplace", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@adress", SqlDbType.VarChar));
            return cmd;
        }

        public static SqlCommand sqlInsert(SqlConnection c)
        {
            SqlCommand cmd = c.CreateCommand();
            cmd.CommandText = SQL_INSERT;
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@birthday", SqlDbType.DateTime));
            cmd.Parameters.Add(new SqlParameter("@birthplace", SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter("@adress", SqlDbType.VarChar));
            return cmd;
        }
    }
}