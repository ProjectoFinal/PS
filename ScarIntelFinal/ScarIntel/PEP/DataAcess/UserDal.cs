using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Logic;

namespace DataAcess
{
    public class UserDal : AbstractDal<User>
    {
        public override IEnumerable<User> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    throw e; 
                }

                    var command = new SqlCommand("Select * from _User", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        yield return new User(reader.GetString(0), reader.GetString(1));
                    }
                    reader.Close();
                


            }
        }

        public override User Get(params string[] parameters)
        {
            return
                GetAll().FirstOrDefault(user => user.Name.Equals(parameters[0]) && user.Password.Equals(parameters[1]));
        }
    }
}