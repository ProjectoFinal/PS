using System;
using System.Runtime.Serialization;

namespace Domain.DataContract
{
    [DataContract]
    public class Document
    {
        [DataMember] public Person Person;
        [DataMember] public DocumentType Type;
        [DataMember] public String code;
        [DataMember] public DateTime emission_date;
        [DataMember] public string emission_local;
        [DataMember] public DateTime expiration_date;
        [DataMember] public int id;

        public Document()
        {
            Type = new DocumentType();
            Person = new Person();
        }

        public Document(int id, DocumentType type, Person person, String s, DateTime emission, DateTime expiration,
            String emissionLocal)
        {
            this.id = id;
            Type = type;
            Person = person;
            code = s;
            emission_date = emission;
            expiration_date = expiration;
            emission_local = emissionLocal;
        }
    }
}