using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Domain.DataContract
{
    [DataContract]
    public class PersonSearchParams
    {

        public const int parameters_count = 4;  
        public const int Name       = 0;  
        public const int NIF       = 1;  
        public const int Birthday   = 2;
        public const int Birthplace = 3;


        [DataMember] public int size;
        [DataMember] public int page;
        [DataMember] public String[] filters = new String [parameters_count];

        public void SetName(String name)
        {
            filters[Name] = name; 
        }

        public void SetNIF(String nif)
        {
            filters[NIF] = nif; 
        }

        public void SetBirthday (String birthday)
        {
            filters[Birthday] = birthday;
        }

        public void SetBirthPlace(String birthPlace)
        {
            filters[Birthplace] = birthPlace;
        }


    }

    [DataContract]
    public class PersonSearchResult
    {
        [DataMember]
        public int size;
        [DataMember]
        public int page;
        [DataMember]
        public Person[] result;
    }
}
