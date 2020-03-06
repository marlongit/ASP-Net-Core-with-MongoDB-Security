using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudingGenerateSecurity.Service
{
    public class ConnectionService
    {
        private const string sufixEntity = "Entity";
        private static ConnectionService _instace;

        public static ConnectionService Instance
        {
            get
            {
                if (_instace == null)
                {
                    lock (typeof(ConnectionService))
                    {
                        _instace = new ConnectionService();
                    }
                }
                return _instace;
            }
        }

        private MongoClient MongoClient { get; }
        private IMongoDatabase MongoDatabase { get; }

        private ConnectionService()
        {
            MongoClient = new MongoClient("ConnectionString");
            MongoDatabase = MongoClient.GetDatabase("DataBase");
        }

        public void Create<T>(T security) where T : class
        {
            MongoDatabase.GetCollection<T>(typeof(T).Name.Split(ConnectionService.sufixEntity)[0]).InsertOne(security);
        }

        public void CreateAsync<T>(T security) where T : class
        {
            MongoDatabase.GetCollection<T>(typeof(T).Name.Split(ConnectionService.sufixEntity)[0]).InsertOneAsync(security);
        }

        public string Update<T>(Expression<Func<T, bool>> filter, T security) where T : class
        {
            var obj = MongoDatabase.GetCollection<T>(typeof(T).Name.Split(ConnectionService.sufixEntity)[0]).ReplaceOne(filter, security);
            return obj.UpsertedId.AsString;
        }

        public IList<T> ListAll<T>()
        {
            var list = MongoDatabase.GetCollection<T>(typeof(T).Name.Split(ConnectionService.sufixEntity)[0]);
            return list.AsQueryable<T>().ToList();
        }

        public IList<T> ListAllByFilter<T>(Expression<Func<T, bool>> filter)
        {
            var list = MongoDatabase.GetCollection<T>(typeof(T).Name.Split(ConnectionService.sufixEntity)[0]).AsQueryable().Where(filter).ToList();
            return list;
        }

        public Task<List<T>> ListByFilterAsync<T>(Expression<Func<T, bool>> filter)
        {
            var list = MongoDatabase.GetCollection<T>(typeof(T).Name.Split(ConnectionService.sufixEntity)[0]).FindAsync(filter).Result.ToListAsync();
            return list;
        }

        public T FindByFilter<T>(Expression<Func<T, bool>> filter)
        {
            var entity = MongoDatabase.GetCollection<T>(typeof(T).Name.Split(ConnectionService.sufixEntity)[0]).Find(filter).FirstOrDefault();
            return entity;
        }

        public Task<T> FindByFilterAsync<T>(Expression<Func<T, bool>> filter)
        {
            var entity = MongoDatabase.GetCollection<T>(typeof(T).Name.Split(ConnectionService.sufixEntity)[0]).FindAsync(filter).Result.FirstOrDefaultAsync();
            return entity;
        }
    }
}

