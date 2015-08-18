using System;
using System.Runtime.Serialization;

namespace Domain.DataContract
{
    [DataContract]
    public class BiometricType
    {
        [DataMember] public Person Person;


        public BiometricType(Byte[] image, String name, int id, Person person)
        {
            data = image;
            Name = name;
            Id = id;
            Person = person;
        }

        public BiometricType()
        {
            Person = new Person();
            // TODO: Complete member initialization
        }

        [DataMember]
        public Byte[] data { get; set; }

        [DataMember]
        public String Name { set; get; }

        [DataMember]
        public int Id { set; get; }
    }
}