using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Logic;

namespace DataAcess
{
    public class UserAssigmentDal : AbstractDal<UserAssingnment>
    {
        private readonly RoleDal roleDal = new RoleDal();
        private readonly UserDal usersDal = new UserDal();

        public override IEnumerable<UserAssingnment> GetAll()
        {
            IEnumerable<User> users = usersDal.GetAll();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                foreach (User u in users)
                {
                    var roles = new List<Role>();
                    var command = new SqlCommand("Select * from _UserAssignment where name=@name", connection);
                    command.Parameters.Add("@name", SqlDbType.VarChar);
                    command.Parameters["@name"].Value = u.Name;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        roles.Add(roleDal.Get(reader.GetString(1)));
                    }
                    reader.Close();
                    yield return new UserAssingnment(u, roles);
                }
            }
        }

        public override UserAssingnment Get(params string[] parameters)
        {
            return GetAll().FirstOrDefault(target => target.user.Name.Equals(parameters[0]));
        }
    }
}