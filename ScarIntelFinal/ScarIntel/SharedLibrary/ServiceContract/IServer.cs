using System;
using System.ServiceModel;
using Domain;
using Domain.DataContract;
using Domain.FaultContract;
using Domain.ServiceContract;

namespace SarcIntelService
{
    [ServiceContract(Namespace = "BrokerService", Name = "ServerService")]
    public interface IServer
    {
        /*******LOGIN**************/
        /*[FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        string SignIn(Credential credential);

        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        void SignOut(string token);*/


        /*****************************************/


        /*****************GET******************/
        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        DocumentType[] GetAllDocumentType();


        [FaultContract(typeof(IllegalAccess))]
        [FaultContract(typeof(ServerError))]
        [OperationContract(IsOneWay = false)]
        Person[] GetAllPersonByIdRegist(int idRegist);
      
        
        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        Document[] GetAllDocumentPerson(int idperson);


        [FaultContract(typeof(IllegalAccess))]
        [FaultContract(typeof(ServerError))]
        [OperationContract(IsOneWay = false)]
        Regist[] GetAllRegists();

        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        CrimeType[] GetAllCrimeTypeByRegist(int idRegist);


        
        
        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        Regist[] GetRegists(int person_id);


        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        int GetNumberOfParticipants(int regist);




        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        CrimeType[] GetAllCrimeType();

        
        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        PersonSearchResult GetAllPerson(PersonSearchParams personSearchParams);

        
        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        Person GetPerson(int id);

        

        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        BiometricType[] GetBiometricTypes(int person_id);


        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        BiometricType GetBiometricType(int person_id, String name);
        /*****************************************/


        /*******  INSERT OPERATIONS *********/

        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        int InsertParticipant(Participant val);

        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        int InsertPerson(Person val);

        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        int InsertRegist(Regist val);

        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        int InsertBiometricType(BiometricType val);

        [FaultContract(typeof (IllegalAccess))]
        [FaultContract(typeof (ServerError))]
        [OperationContract(IsOneWay = false)]
        int InsertDocument(Document doc);
    }
}