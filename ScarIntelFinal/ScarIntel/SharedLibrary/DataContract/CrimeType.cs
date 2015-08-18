using System;
using System.Runtime.Serialization;

namespace Domain.DataContract
{
    [DataContract]
    public class CrimeType
    {
        public CrimeType()
        {
        }

        public CrimeType(int id, String name)
        {
            Name = name;
            Id = id;
        }

        [DataMember]
        public String Name { set; get; }

        [DataMember]
        public int Id { set; get; }
    }
}