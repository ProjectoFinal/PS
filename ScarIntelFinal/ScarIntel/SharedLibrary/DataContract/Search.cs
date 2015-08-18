using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataContract
{
    [DataContract]
    public class SearchParams
    {
        [DataMember] public int size;
        [DataMember] public int page;
        [DataMember] public String[] filters; 
    }

    [DataContract]
    public class SearchResult
    {
        [DataMember]
        public int size;
        [DataMember]
        public int page;
        [DataMember]
        public Person[] result;
    }
}
