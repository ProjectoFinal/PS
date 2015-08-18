using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Logic;

namespace DataAcess
{
    public class PermissionDal : AbstractDal<Permission>
    {
        public override IEnumerable<Permission> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("Select * from _Permission", connection);
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    yield return new Permission(reader.GetString(0), reader.GetString(1));
                }
                reader.Close();
            }
        }

        public override Permission Get(params string[] parameters)
        {
            return
                GetAll()
                    .FirstOrDefault(
                        target => target.Method.Equals(parameters[0]) && target.Resource.Equals(parameters[1]));
        }
    }
}