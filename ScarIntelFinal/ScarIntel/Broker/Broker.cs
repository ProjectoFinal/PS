using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Domain;
using PEP;
using SharedLibrary;
using SharedLibrary.Model;
using SqlMapper;
using SqlMapper.DataMapper;

namespace Broker
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BrokerServer : IServer //,Dispose
    {
        private readonly AutenticationProvider authProvider;
        private long accesscounter;
        private long actives_users;

        public BrokerServer()
        {
            authProvider = new AutenticationProvider();
            actives_users = 0;
        }

        //
        // Get an array of Regist associated with a person id 
        //
        public Regist[] GetRegists(int person_id)
        {
            try
            {
                using (var connect = new Connect())
                {
                    SqlConnection sqlconn = connect.GetConnection();
                    var registMapper = new RegistDataMapper(sqlconn);
                    var crimeTypeMapper = new CrimeTypeDataMapper(sqlconn);
                    var partiMapper = new ParticipantDataMapper(sqlconn);

                    IEnumerable<Participant> participants = partiMapper.GetAll().Where(p => p.Person.Id == person_id);
                    var regists = new List<Regist>();


                    foreach (Participant part in participants)
                    {
                        regists.Add(registMapper.GetById(part.Regist.Id));
                    }

                    foreach (Regist r in regists)
                    {
                        foreach (Participant p in partiMapper.GetAll().Where(part => part.Regist.Id == r.Id))
                        {
                            r.AddParticipant(p);
                        }
                        r.SetParticipants();
                    }

                    return regists.ToArray();
                }
            }
            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }

        }


        //
        // Get an array of all Person in the database
        //
        public Person[] GetAllPerson(params String[] filters)
        {
            try
            {
                using (var connect = new Connect())
                {
                    SqlConnection sqlconn = connect.GetConnection();
                    var personDoa = new PersonDataMapper(sqlconn);

                    //  personDoa.GetAll().Where(p => p.Name == filters[0] && p.Birthplace == filters[1]);
                    return personDoa.GetAll().ToArray();
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
            if (!IsAuthorized("POST", "/Person"))
                throw new FaultException<IllegalAcess>( new IllegalAcess());

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

        public void InsertParticipant(Participant val)
        {
            try
            {
                using (var c = new Connect())
                {

                    c.BeginTrx();
                    var partiMapper = new ParticipantDataMapper(c.GetConnection());
                    partiMapper.SetTransaction(c.Transaction);
                    partiMapper.Insert(val);
                    c.Commit();

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
        

        public Document[] GetAllDocumentTypebyPerson(int idperson)
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

        public void InsertDocument(Document doc)
        {
            try
            {
                using (var connect = new Connect())
                {
                    connect.BeginTrx();
                    SqlConnection sqlconn = connect.GetConnection();
                    var docmapper = new DocumentDataMapper(sqlconn);
                    docmapper.SetTransaction(connect.Transaction);
                    docmapper.Insert(doc);
                    connect.Commit();
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

        public string SignIn(Credential credential)
        {
            try
            {
                string user = credential.user;
                string pass = credential.pass;
                if (PdpUserProvider.IsValidUser(user, pass))
                {
                    Interlocked.Increment(ref actives_users);
                    Interlocked.Increment(ref accesscounter);

                    Session session = new Session();
                    session.user = user;

                    using (var connect = new Connect())
                    {
                        connect.BeginTrx();
                        SqlConnection sqlconn = connect.GetConnection();
                        var smapper  = new SessionDataMapper(sqlconn);
                        smapper.SetTransaction(connect.Transaction);
                        //smapper.Insert(session);
                    }
                    


                    return authProvider.AutenticateUser(credential);
                }
                return null;
            }

            catch (Exception exception)
            {
                throw new FaultException<ServerError>(new ServerError());
            }
        }

        public void SignOut(string token)
        {
            if (authProvider.RemoveAuthToken(token))
            {
                Interlocked.Decrement(ref actives_users);
            }
        }

        private bool IsAuthorized(string method, string resource)
        {
            string token = null;
            MessageProperties msgProp = OperationContext.Current.IncomingMessageProperties;
            var ctxProperty = msgProp[ContextMessageProperty.Name] as ContextMessageProperty;
            if (ctxProperty.Context.ContainsKey("AccessToken"))
            {
                token = ctxProperty.Context["AccessToken"];
                AutenticationToken authToken = authProvider.GetAutenticationToken(token);

                if (authToken == null || authToken.expire < DateTime.Now)
                    return false;

                return PdpUserProvider.IsAutorized(authToken.credential.user, method, resource);
            }
            return false;
        }

        public void PrintCurrentUsers()
        {
            foreach (string token in authProvider.tokenDictionary.Keys)
            {
                String user = authProvider.GetAutenticationToken(token).credential.user;
                Console.WriteLine("Token: {0}  ->  User: {1}", token, user);
            }
        }
    }
}