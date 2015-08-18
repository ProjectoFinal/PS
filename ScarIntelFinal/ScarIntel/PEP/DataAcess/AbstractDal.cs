using System.Collections.Generic;
using System.IO;
//using SqlMapper;

namespace DataAcess
{
    public abstract class AbstractDal<T> : IDal<T>
    {
        //protected string ConnectionString = System.IO.File.ReadAllText(@".\config.txt");
        protected string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConn1"].ConnectionString;
        // "Database=BacIntel;User=BAC;password=1561231486";
        public abstract IEnumerable<T> GetAll();
        public abstract T Get(params string[] parameters);
    }
}