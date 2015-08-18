using System;
using System.Runtime.Serialization;

namespace Domain.DataContract
{
    [DataContract]
    public class Person
    {
        public Person()
        {
        }

        public Person(int id, String name, DateTime birth, String birthplace, String address)
        {
            Name = name;
            Id = id;
            Birthday = birth;
            Birthplace = birthplace;
            Address = address;
        }

        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public String Name { set; get; }

        [DataMember]
        public DateTime Birthday { set; get; }

        [DataMember]
        public String Birthplace { set; get; }

        [DataMember]
        public String Address { set; get; }
    }
}