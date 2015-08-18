using System;
using System.Runtime.Serialization;

namespace Domain.DataContract
{
    [DataContract]
    public class DocumentType
    {
        public DocumentType()
        {
        }

        public DocumentType(int id, String name)
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