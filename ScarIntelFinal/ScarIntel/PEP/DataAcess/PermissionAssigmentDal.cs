using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Logic;

namespace DataAcess
{
    public class PermissionAssigmentDal : AbstractDal<PermissionAssigment>
    {
        private readonly RoleDal roleDal = new RoleDal();


        public override IEnumerable<PermissionAssigment> GetAll()
        {
            IEnumerable<Role> roles = roleDal.GetAll();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                foreach (Role role in roles)
                {
                    var permissions = new List<Permission>();
                    var command = new SqlCommand("Select * from _PermissionAssignment where mainRole=@role", connection);
                    command.Parameters.Add("@role", SqlDbType.VarChar);
                    command.Parameters["@role"].Value = role.Name;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for (Role r = role; r != null; r = r.Junior)
                            permissions.Add(GetRolePermission(r.Name));
                    }
                    reader.Close();
                    yield return new PermissionAssigment(role, permissions);
                }
            }
        }

        public override PermissionAssigment Get(params string[] parameters)
        {
            return GetAll().FirstOrDefault(target => target.Role.Name.Equals(parameters[0]));
        }

        public Permission GetRolePermission(string roleName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("Select * from _PermissionAssignment where mainRole=@role", connection);
                command.Parameters.Add("@role", SqlDbType.VarChar);
                command.Parameters["@role"].Value = roleName;
                SqlDataReader reader = command.ExecuteReader();
                return reader.Read() ? new Permission(reader.GetString(0), reader.GetString(1)) : null;
            }
        }
    }
}