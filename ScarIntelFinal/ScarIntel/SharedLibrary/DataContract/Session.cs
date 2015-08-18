using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataContract
{
    public class Session
    {
        public int Id;
        public String user;
        public String token;
        public readonly DateTime emission_time;
        public DateTime expiration_time;

        public Session()
        {
            emission_time = DateTime.Now;
            expiration_time = emission_time.AddHours(2);
        }
    }
}
