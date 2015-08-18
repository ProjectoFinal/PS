using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.FaultContract;
using Domain.ServiceContract;
using PEP;
using SqlMapper;
using SqlMapper.DataMapper;

namespace SarcIntelService
{
    class UserNamePassValidator :
            System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new FaultException("userName cannot be null");
            }

            bool valid = false; 

            try
            {
                valid = PdpUserProvider.IsValidUser(userName, password);
            }

            catch (Exception exception)
            {
                throw new Exception("Internal Server Error");
            }

            if( !valid )
                throw new Exception("Incorrect credentials");

            
        }
    }    
}
