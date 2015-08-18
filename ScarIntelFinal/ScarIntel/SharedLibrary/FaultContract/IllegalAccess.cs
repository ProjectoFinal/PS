using System.Runtime.Serialization;

namespace Domain.FaultContract
{
    [DataContract]
    public class IllegalAccess
    {
        public readonly string message = "Illegal Access "; 

    }
}