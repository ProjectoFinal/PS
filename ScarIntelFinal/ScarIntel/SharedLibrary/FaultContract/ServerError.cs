using System.Runtime.Serialization;

namespace Domain.ServiceContract
{
    [DataContract]
    public class ServerError
    {
        public readonly string message = "Server Error "; 
    }
}