using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using Domain;
using Domain.DataContract;
using Domain.FaultContract;
using Domain.ServiceContract;
using PEP;
using SqlMapper;
using SqlMapper.DataMapper;

namespace SarcIntelService
{
    
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Server : IServer
    {
        public Server()
        {
            /*//23e2e5d87aa03c158390b4f3d6ac034cb376eda2
            //{4E07D329-17C0-4674-B51B-24D5240B76FD}

            string certhash = "23e2e5d87aa03c158390b4f3d6ac034cb376eda2";
            string appid = Assembly.GetExecutingAssembly().GetType().GUID.ToString();



            String s = "http add sslcert ipport=0.0.0.0:8080 certhash={0} appid={1}";
            ProcessStartInfo procStartInfo = new ProcessStartInfo("netsh", s);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process.Start(procStartInfo);

            ;;*/
        }

        //
        // Get an array of Regist associated with a person id 
        //
        public Person[] GetAllPersonByIdRegist(int idRegist)
        {
            using (var connect = new Connect())
            {
                SqlConnection sqlconn = connect.GetConnection();
                var personMapper = new PersonDataMapper(sqlconn);
                var participants = personMapper.GetAllPersonByRegist(idRegist);

                return participants.ToArray();
            }
        }

        public CrimeType[] GetAllCrimeTypeByRegist(int idRegist)
        {

            using (var connect = new Connect())
            {
                SqlConnection sqlconn = connect.GetConnection();
                RegistDataMapper registDataMapper = new RegistDataMapper(sqlconn);
                var crimes = registDataMapper.GetCrimeType(idRegist);

                return crimes.ToArray();
            }


        }


        public Regist[] GetAllRegists()
        {
            try
            {
                using (var connect = new Connect())
                {
                    SqlConnection sqlconn = connect.GetConnection();
                    var registMapper = new RegistDataMapper(sqlconn);

                    var regists = registMapper.GetAll();
                    if (regists == null) return null;
                    return regists.ToArray();
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }

        }

        public int GetNumberOfParticipants(int idRegist)
        {
            try
            {
                using (var connect = new Connect())
                {
                    SqlConnection sqlconn = connect.GetConnection();
                    var partiMapper = new ParticipantDataMapper(sqlconn);

                    return partiMapper.GetCountParticipants(idRegist);

                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }


        public Regist[] GetRegists(int person_id)
        {
            try
            {
                using (var connect = new Connect())
                {
                    SqlConnection sqlconn = connect.GetConnection();
                    var registMapper = new RegistDataMapper(sqlconn);

                    IEnumerable<Regist> regist = registMapper.GetRegistsByPerson(person_id);

                    return regist.ToArray();
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }


        //
        // Get an array of all Person that match the SearchParams parameters in the database
        //

        // Argumentos 1º name , 2º birthday , 3º nacionalidade 
        public SearchResult GetAllPerson(SearchParams searchParams)
        {


            try
            {
                string[] filters = searchParams.filters;

                using (var connect = new Connect())
                {
                    SqlConnection sqlconn = connect.GetConnection();

                    var personDoa = new PersonDataMapper(sqlconn);

                    SearchResult sr = new SearchResult();

                    if (filters.Length < 3)
                    {
                        sr.result = personDoa.GetAll().ToArray();
                        return sr;
                    }
                    sr.result = personDoa.GetAll().Where(p => p.Birthplace == filters[2]).ToArray();
                    return sr;
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }

        // 
        // Get a Person instance identify by his id 
        //
        public Person GetPerson(int id)
        {

            try
            {
                using (var c = new Connect())
                {
                    var personDoa = new PersonDataMapper(c.GetConnection());
                    return personDoa.GetById(id);
                }
            }

            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }

        //
        //  Insert a Person
        //
        public int InsertPerson(Person val)
        {

            try
            {
                using (var c = new Connect())
                {
                    c.BeginTrx(IsolationLevel.ReadCommitted);
                    var personDoa = new PersonDataMapper(c.GetConnection());
                    personDoa.SetTransaction(c.Transaction);
                    int result = personDoa.Insert(val);
                    c.Commit();
                    return result;
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }

        //
        // Insert a Regist
        //
        public int InsertRegist(Regist val)
        {
            try
            {
                using (var c = new Connect())
                {
                    c.BeginTrx();
                    var registMapper = new RegistDataMapper(c.GetConnection());
                    registMapper.SetTransaction(c.Transaction);
                    int result = registMapper.Insert(val);
                    c.Commit();
                    return result;
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }

        public int InsertParticipant(Participant val)
        {
            try
            {
                using (var c = new Connect())
                {

                    c.BeginTrx();
                    var partiMapper = new ParticipantDataMapper(c.GetConnection());
                    partiMapper.SetTransaction(c.Transaction);
                    int ret = partiMapper.Insert(val);
                    c.Commit();
                    return ret;

                }
            }

            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }

        public CrimeType[] GetAllCrimeType()
        {
            try
            {
                using (var c = new Connect())
                {
                    SqlConnection sqlconn = c.GetConnection();
                    var crimeMapper = new CrimeTypeDataMapper(sqlconn);
                    IEnumerable<CrimeType> list = crimeMapper.GetAll();
                    return list.ToArray();
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }

        public DocumentType[] GetAllDocumentType()
        {
            try
            {
                using (var c = new Connect())
                {
                    SqlConnection sqlconn = c.GetConnection();
                    var mapper = new DocumentTypeDataMapper(sqlconn);
                    IEnumerable<DocumentType> list = mapper.GetAll();
                    return list.ToArray();
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }

        }


        public Document[] GetAllDocumentPerson(int idperson)
        {
            try
            {
                using (var c = new Connect())
                {
                    SqlConnection sqlconn = c.GetConnection();

                    var mapper = new DocumentDataMapper(sqlconn);
                    IEnumerable<Document> list = mapper.GetAll().Where(d => d.Person.Id == idperson);
                    return list.ToArray();
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }

        }

        public BiometricType GetBiometricType(int person_id, string name)
        {

            try
            {
                name = name.ToLower();
                using (var connect = new Connect())
                {
                    SqlConnection sqlconn = connect.GetConnection();
                    var bioDao = new BiometricTypeDataMapper(sqlconn);


                    return bioDao.GetAll().FirstOrDefault(
                        bio =>
                            bio.Person.Id == person_id && bio.Name.ToLower().Equals(name));
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }

        public int InsertBiometricType(BiometricType val)
        {
            try
            {
                using (var connect = new Connect())
                {
                    connect.BeginTrx();
                    SqlConnection sqlconn = connect.GetConnection();
                    var bioDao = new BiometricTypeDataMapper(sqlconn);
                    bioDao.SetTransaction(connect.Transaction);
                    int result = bioDao.Insert(val);
                    connect.Commit();

                    return result;
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }

        }

        public int InsertDocument(Document doc)
        {
            try
            {
                using (var connect = new Connect())
                {

                    connect.BeginTrx();
                    SqlConnection sqlconn = connect.GetConnection();
                    var docmapper = new DocumentDataMapper(sqlconn);
                    docmapper.SetTransaction(connect.Transaction);
                    int ret = docmapper.Insert(doc);
                    connect.Commit();

                    return ret;
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }

        }

        public BiometricType[] GetBiometricTypes(int person_id)
        {
            try
            {
                using (var connect = new Connect())
                {
                    SqlConnection sqlconn = connect.GetConnection();
                    var bioDao = new BiometricTypeDataMapper(sqlconn);
                    return bioDao.GetAll().Where(bio => bio.Person.Id == person_id).ToArray();
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }

        }

    }

}
