using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MVC_2020_Database
{
    public class BaseService
    {
        public static string connection = "";
        public static OrmLiteConnectionFactory dbFactory { get; set; }

        public static void startConnection(string connectionString)
        {
            connection = connectionString;
            dbFactory = new OrmLiteConnectionFactory(connection, SqlServerDialect.Provider);
        }
        
        public static List<T> Get<T>()
        {
                     
            using (var db = dbFactory.Open())
            {
                var result = db.Select<T>();
                return result;
            }
        }

        public static bool Save<T>(T e)
        {
            using (var db = dbFactory.Open())
            {
                return db.Save(e);
            }
        }

        public static bool Delete<T>(T e)
        {
            using (var db = dbFactory.Open())
            {
                return db.Save(e);
            }

        }

        public static IEnumerable<T> Query<T>(string sql)
        {


            using (var db = dbFactory.Open())
            {
                var result = db.Query<T>(sql);
                return result;
            }
        }

        public static IEnumerable<T> Query<T>(string sql, object camposFiltro)
        {


            using (var db = dbFactory.Open())
            {
                var result = db.Query<T>(sql, camposFiltro);
                return result;
            }
        }

        public static List<T> Select<T>()
        {
            using (var db = dbFactory.Open())
            {
                return db.Select<T>();
            }
        }

        public static List<T> Select<T>(Expression<Func<T, bool>> filtro)
        {
            using (var db = dbFactory.Open())
            {
                return db.Select(filtro);
            }
        }

        public static bool Any<T>()
        {
            return Count<T>() > 0;
        }

        public static bool Any<T>(Expression<Func<T, bool>> filtro)
        {
            return Count<T>(filtro) > 0;
        }

        public static long Count<T>()
        {
            using (var db = dbFactory.Open())
                return db.Count<T>();
        }

        public static long Count<T>(Expression<Func<T, bool>> filtro)
        {
            using (var db = dbFactory.Open())
                return db.Count<T>(filtro);
        }

        public static void Delete<T>(Expression<Func<T, bool>> filtro) where T : new()
        {
            using (var db = dbFactory.Open())
                db.Delete<T>(filtro);
        }

        public static void DeleteById<T>(object id) where T : new()
        {
            using (var db = dbFactory.Open())
                db.DeleteById<T>(id);
        }

        public static void DeleteByIds<T>(IEnumerable<int> ids) where T : new()
        {
            using (var db = dbFactory.Open())
                db.DeleteByIds<T>(ids);
        }
    }
}
