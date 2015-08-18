using System.Runtime.Serialization;
using Domain;

namespace Domain.DataContract
{
    [DataContract]
    public class Participant
    {
        [DataMember] public Person Person;
        [DataMember] public Regist Regist;


        public Participant(int id, Person person, Regist regist)
        {
            Person = person;
            Id = id;
            Regist = regist;
        }

        public Participant()
        {
            Person = new Person();
            Regist = new Regist();
            // TODO: Complete member initialization
        }

        [DataMember]
        public int Id { set; get; }
    }
}