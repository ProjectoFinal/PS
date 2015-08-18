using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;

namespace SqlMapper
{
    public interface IConnect : IDisposable
    {
        SqlConnection GetConnection();
    }

    public class Connect : IConnect
    {
        //private static string DefaultString = "Database=BacIntel;User=BAC;password=1561231486";
        public static string DefaultString =  "Database=BacIntel; Integrated Security=true;";

        public SqlTransaction Transaction;
        private SqlConnection c;

        static Connect()
        {
            DefaultString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConn1"].ConnectionString;
        }


        public SqlConnection GetConnection()
        {
            try
            {
                if (c == null)
                {
                    c = new SqlConnection(DefaultString);
                    c.OpenAsync().Wait();
                    // http://blogs.msdn.com/b/pfxteam/archive/2009/10/15/9907713.aspx
                    // Wait prefers to execute useful work rather than blocking, and it has useful work at its finger tips
                }
                return c;
            }
            catch (Exception e)
            {
                throw e; 
            }
            return null;
        }

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
            if (c != null)
            {
                c.Dispose();
                c = null;
            }
        }

        public void BeginTrx()
        {
            if (Transaction != null) throw new InvalidOperationException("Transaction already initialized!");
            Transaction = GetConnection().BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTrx(IsolationLevel level)
        {
            if (Transaction != null) throw new InvalidOperationException("Transaction already initialized!");
            Transaction = GetConnection().BeginTransaction(level);
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Commit()
        {
            Transaction.Commit();
        }    
    }
}