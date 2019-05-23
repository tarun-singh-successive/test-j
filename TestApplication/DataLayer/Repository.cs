using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Data.Entity.Migrations;

namespace TestApplication.DataLayer
{
    public class Repository : IRepository
    {
        [ThreadStatic]
        private static Repository _repository;

        private DataContext DataContext { get; set; }

        public static Repository Instance
        {
            get { return _repository ?? (_repository = new Repository()); }
        }

        public static int PageSize { get { return 5; } }

        private Repository()
        {
            DataContext = new DataContext();
        }

        private DbSet<T> DbSet<T>() where T : class
        {
            return DataContext.Set<T>();
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>().AsQueryable();
        }

        public IQueryable<T> All<T>(params string[] includes) where T : class
        {
            var dbSet = DbSet<T>().AsQueryable();

            if (includes == null || !includes.Any()) return dbSet;

            foreach (var include in includes)
            {
                dbSet = dbSet.Include(include);
            }
            return dbSet;
        }

        public static IQueryable<T> Table<T>() where T : class
        {
            return Instance.DbSet<T>();
        }

        public IQueryable<T> All<T>(int index, int size) where T : class
        {
            int skipCount = index * size;
            var resultSet = DbSet<T>().AsQueryable();
            resultSet = skipCount == 0 ? resultSet.Take(size) : resultSet.Skip(skipCount).Take(size);
            return resultSet.AsQueryable();
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate, params string[] includes) where T : class
        {
            var dbSet = DbSet<T>().Where(predicate).AsQueryable();

            if (includes == null || !includes.Any()) return dbSet;

            foreach (var include in includes)
            {
                dbSet = dbSet.Include(include);
            }
            return dbSet;
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50) where T : class
        {
            int skipCount = index * size;
            var resetSet = predicate != null ? DbSet<T>().Where(predicate).AsQueryable() : DbSet<T>().AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public T Find<T>(params object[] keys) where T : class
        {
            return DbSet<T>().Find(keys);
        }

        public T Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return DbSet<T>().Where(predicate).FirstOrDefault();
        }

        public T Insert<T>(T entity, bool commit = true) where T : class
        {
            var newEntry = DbSet<T>().Add(entity);
            if (commit)
            {
                DataContext.SaveChanges();
            }
            return newEntry;
        }

        public int Update<T>(T entity) where T : class
        {
            DbSet<T>().AddOrUpdate(entity);
            return DataContext.SaveChanges();
        }

        public void DetachEntity<T>(T entity) where T : class
        {
            DataContext.Entry(entity).State = EntityState.Detached;
        }

        public void RefreshEntity<T>(T entity) where T : class
        {
            DataContext.Entry(entity).Reload();
        }

        public int Delete<T>(T entity) where T : class
        {
            DbSet<T>().Remove(entity);
            return DataContext.SaveChanges();
        }

        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
            {
                DbSet<T>().Remove(obj);
            }
            return DataContext.SaveChanges();
        }

        public int Count<T>() where T : class
        {
            return DbSet<T>().Count();
        }

        public int Count<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return DbSet<T>().Count(predicate);
        }

        public bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return DbSet<T>().Count(predicate) > 0;
        }

        public DataSet ExecuteReader(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var ds = new DataSet();
            var cmd = DataContext.Database.Connection.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            if (parameters != null && parameters.Count() != 0)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            var isMyConnection = false;
            if (DataContext.Database.Connection.State != ConnectionState.Open)
            {
                DataContext.Database.Connection.Open();
                isMyConnection = true;
            }
            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    ds.Load(reader, LoadOption.OverwriteChanges, "Results");
                }
            }
            finally
            {
                if (isMyConnection)
                    DataContext.Database.Connection.Close();
            }
            return ds;
        }

        public IEnumerable<T> ExecuteReader<T>(string commandText) where T : class
        {
            return DataContext.Database.SqlQuery<T>(commandText);
        }

        public int ExecuteSqlCommnad(string commandText, params SqlParameter[] parameters)
        {
            return DataContext.Database.ExecuteSqlCommand(commandText, parameters);
        }

        public IEnumerable<T> ExecuteFunction<T>(string commandText, params SqlParameter[] parameters) where T : struct
        {
            return DataContext.Database.SqlQuery<T>(commandText, parameters);
        }

        public IEnumerable<T> ExecuteReader<T>(string commandText, params SqlParameter[] parameteres) where T : class
        {
            return DataContext.Database.SqlQuery<T>(commandText, parameteres);
        }

        public IEnumerable<T> ExecuteReader<T>(string commandText, params object[] parameteres) where T : class
        {
            return DataContext.Database.SqlQuery<T>(commandText, parameteres);
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        //PagedView
        public void Filter<T>(Expression<Func<T, bool>> predicate, PagedView<T> pagedView) where T : class
        {
            var sortExpression = string.Format("{0} {1}", pagedView.Sort ?? "Id", pagedView.SortDir ?? "ASC");

            var dbSet = DbSet<T>().Where(predicate).OrderBy(sortExpression).AsQueryable();

            pagedView.TotalRecords = dbSet.Count();

            pagedView.RecordIndex = ((pagedView.Page ?? 1) - 1) * PageSize;

            pagedView.ResultSet = dbSet.Skip(pagedView.RecordIndex).Take(PageSize);
        }

        public Expression<Func<TObj, bool>> BuildExpression<TObj, TVal>(TVal val)
        {
            var parameter = Expression.Parameter(typeof(TObj), "o");
            var valExpression = Expression.Constant(val, typeof(TVal));
            dynamic body = Expression.Constant(false, typeof(bool));

            var properties = typeof(TObj).GetProperties()
                                         .Where(p => p.PropertyType == typeof(TVal));
            foreach (var property in properties)
            {
                var propertyExpression = Expression.Property(parameter, property);
                var equalExpression = Expression.Equal(propertyExpression, valExpression);
                body = Expression.Or(body, equalExpression);
            }

            return Expression.Lambda<Func<TObj, bool>>(body, parameter);
        }

        public static void Dispose()
        {
            if (_repository != null)
            {
                _repository.DataContext.Dispose();
                _repository.DataContext = null;
                _repository = null;
            }
        }
    }
}
