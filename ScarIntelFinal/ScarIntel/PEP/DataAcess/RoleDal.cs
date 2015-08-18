using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Logic;

namespace DataAcess
{
    public class RoleDal : AbstractDal<Role>
    {
        public Role GetJunior(string roleName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("select * from _Role where name = @name", connection);
                command.Parameters.Add("@name", SqlDbType.VarChar);
                command.Parameters["@name"].Value = roleName;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return new Role(reader.IsDBNull(1) ? null : GetJunior(reader.GetString(1)), reader.GetString(0));
            }
        }

        public override IEnumerable<Role> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("Select * from _Role", connection);
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    yield return
                        new Role(reader.IsDBNull(1) ? null : GetJunior(reader.GetString(1)), reader.GetString(0));
                }
                reader.Close();
            }
        }

        public override Role Get(params string[] parameters)
        {
            return GetAll().FirstOrDefault(role => role.Name.Equals(parameters[0]));
        }
    }
}