using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlMapper
{
    public class SqlEnumerable<T> : IEnumerable<T>
    {
        private readonly bool blocked;
        private readonly SqlCommand stmt;
        private readonly bool where;
        private Func<SqlDataReader, T> creator;
        private string query;

        public SqlEnumerable(SqlCommand cmd, Func<SqlDataReader, T> creator)
        {
            where = false;
            blocked = false;
            stmt = cmd;
            creator = creator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            stmt.CommandText += query;
            SqlDataReader read = stmt.ExecuteReader();

            while (read.Read())
            {
                yield return creator(read);
            }
            read.Close();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public SqlEnumerable<T> Where(string s)
        {
            if (!where)
            {
                query += " where ";
            }
            if (blocked)
            {
                throw new InvalidOperationException(
                    " Não é possivel filtrar depois de aceder a um elemnto do enumeravel ");
            }
            query = query + s;
            return this;
        }
    }
}