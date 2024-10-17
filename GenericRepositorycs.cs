//using Dapper;
//using Microsoft.Data.SqlClient;
//using projectnew.Models;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;

//namespace projectnew.Data
//{
//    public class GenericRepository<T> : IRepository<T> where T : class
//    {
//        private readonly string _connectionString;

//        public GenericRepository(string connectionString)
//        {
//            _connectionString = connectionString;
//        }

//        private IDbConnection Connection
//        {
//            get
//            {
//                return new SqlConnection(_connectionString);
//            }
//        }

//        public IEnumerable<T> GetAll()
//        {
//            using (IDbConnection dbConnection = Connection)
//            {
//                string query = $"SELECT * FROM {typeof(T).Name}";
//                return dbConnection.Query<T>(query);
//            }
//        }

//        public T FindById(int id)
//        {
//            using (IDbConnection dbConnection = Connection)
//            {
//                string query = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";
//                return dbConnection.Query<T>(query, new { Id = id }).FirstOrDefault();
//            }
//        }

//        //public void Add(T entity)
//        //{
//        //    using (IDbConnection dbConnection = Connection)
//        //    {
//        //        var properties = typeof(T).GetProperties().Select(p => p.Name).ToArray();
//        //        var columns = string.Join(", ", properties);
//        //        var values = string.Join(", ", properties.Select(p => "@" + p));

//        //        string query = $"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({values})";
//        //        dbConnection.Execute(query, entity);
//        //    }
//        //}

//        public void Add(T entity)
//        {
//            using (IDbConnection dbConnection = Connection)
//            {
//                var properties = typeof(T).GetProperties()
//                                          .Where(p => p.Name != "Id") // Exclude Id property
//                                          .Select(p => p.Name)
//                                          .ToArray();

//                var columns = string.Join(", ", properties);
//                var values = string.Join(", ", properties.Select(p => "@" + p));

//                string query = $"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({values})";
//                dbConnection.Execute(query, entity);
//            }
//        }


//        public void UpdateById(int id, T entity)
//        {
//            using (IDbConnection dbConnection = Connection)
//            {
//                var properties = typeof(T).GetProperties().Select(p => p.Name).ToArray();
//                var setClause = string.Join(", ", properties.Select(p => p + " = @" + p));

//                string query = $"UPDATE {typeof(T).Name} SET {setClause} WHERE Id = @Id";
//                dbConnection.Execute(query, entity);
//            }
//        }

//        public void DeleteById(int id)
//        {
//            using (IDbConnection dbConnection = Connection)
//            {
//                string query = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";
//                dbConnection.Execute(query, new { Id = id });
//            }
//        }
//    }
//}

using Dapper;
using Microsoft.Data.SqlClient;
using projectnew.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace projectnew.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;

        public GenericRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = $"SELECT * FROM {typeof(T).Name}";
                return dbConnection.Query<T>(query);
            }
        }

        public T FindById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";
                return dbConnection.Query<T>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public void Add(T entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var properties = typeof(T).GetProperties()
                                          .Where(p => p.Name != "Id") 
                                          .Select(p => p.Name)
                                          .ToArray();

                var columns = string.Join(", ", properties);
                var values = string.Join(", ", properties.Select(p => "@" + p));

                string query = $"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({values})";
                dbConnection.Execute(query, entity);
            }
        }

        //public void UpdateById(int id, T entity)
        //{
        //    using (IDbConnection dbConnection = Connection)
        //    {
        //        var properties = typeof(T).GetProperties().Select(p => p.Name).ToArray();
        //        var setClause = string.Join(", ", properties.Select(p => p + " = @" + p));

        //        string query = $"UPDATE {typeof(T).Name} SET {setClause} WHERE Id = @Id";
        //        dbConnection.Execute(query, entity);
        //    }
        //}
        public void UpdateById(int id, T entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var properties = typeof(T).GetProperties()
                                          .Where(p => p.Name != "Id") 
                                          .Select(p => p.Name)
                                          .ToArray();

                var setClause = string.Join(", ", properties.Select(p => p + " = @" + p));

                string query = $"UPDATE {typeof(T).Name} SET {setClause} WHERE Id = @Id";

                // Create a dynamic parameters object and add the Id separately
                var parameters = new DynamicParameters(entity);
                parameters.Add("Id", id);

                dbConnection.Execute(query, parameters);
            }
        }

        public void DeleteById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";
                dbConnection.Execute(query, new { Id = id });
            }
        }
    }
}
