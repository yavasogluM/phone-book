using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Extensions.MongoDB
{
    public interface IBaseRepository<T>
    {
        List<T> GetList();
        Task<List<T>> GetListAsync();
        void InsertItem(T item);
        Task InsertItemAsync(T item);
        T GetById(string id);
        Task<T> GetByIdAsync(string Id);
        void DeleteItem(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        T GetByFilter(Expression<Func<T, bool>> predicate);
        Task DeleteItemAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListByFilterAsync(Expression<Func<T, bool>> predicate);
        List<T> GetListByFilter(Expression<Func<T, bool>> predicate);
        List<T> GetListByFilterDefinition(FilterDefinition<T> filter);
        Task<List<T>> GetListByFilterDefinitionAsync(FilterDefinition<T> filter);

        IMongoCollection<T> Collection { get; set; }

        bool IsExist(Expression<Func<T, bool>> predicate);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate);
        Task<bool> UpdateAsync(string RowId, T item);
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseCollection, new()
    {
        private readonly MongoDBConnectionSetting _mongoDBConnection;

        public readonly IMongoCollection<T> collection;
        public IMongoCollection<T> Collection { get; set; }
        public BaseRepository(MongoDBConnectionSetting mongoDBConnectionSetting)
        {
            _mongoDBConnection = mongoDBConnectionSetting;
            try
            {
                var client = new MongoClient($"mongodb+srv://{_mongoDBConnection.UserName}:{_mongoDBConnection.Password}@cluster0.pjbsp.mongodb.net/{_mongoDBConnection.DBName}?retryWrites=true&w=majority");

                var database = client.GetDatabase(_mongoDBConnection.DBName);
                collection = database.GetCollection<T>(_mongoDBConnection.CollectionName);
                Collection = collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetList() => collection.Find(x => true).ToList();

        public async Task<List<T>> GetListAsync() => await collection.Find(x => true).ToListAsync();

        public List<T> GetListByFilter(Expression<Func<T, bool>> predicate) => Collection.Find(predicate).ToList();

        public async Task<List<T>> GetListByFilterAsync(Expression<Func<T, bool>> predicate) => await collection.Find(predicate).ToListAsync();

        public T GetById(string Id) => collection.Find(x => x.Id == new ObjectId(Id)).FirstOrDefault();

        public async Task<T> GetByIdAsync(string Id) => await collection.Find(x => x.Id == new ObjectId(Id)).FirstAsync();

        public void InsertItem(T item) => collection.InsertOne(item);

        public async Task InsertItemAsync(T item) => await collection.InsertOneAsync(item);

        public T GetByFilter(Expression<Func<T, bool>> predicate) => collection.Find(predicate).FirstOrDefault();

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate) => await collection.Find(predicate).FirstOrDefaultAsync();

        public void DeleteItem(Expression<Func<T, bool>> predicate) => collection.DeleteOne(predicate);

        public async Task DeleteItemAsync(Expression<Func<T, bool>> predicate) => await collection.DeleteOneAsync(predicate);

        public List<T> GetListByFilterDefinition(FilterDefinition<T> filter) => collection.Find(filter).ToList();

        public async Task<List<T>> GetListByFilterDefinitionAsync(FilterDefinition<T> filter) => await collection.Find(filter).ToListAsync();

        public bool IsExist(Expression<Func<T, bool>> predicate) => collection.Find(predicate).Any();

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate) => await collection.Find(predicate).AnyAsync();


        public async Task<bool> UpdateAsync(string RowId, T item)
        {
            try
            {
                var objId = new ObjectId(RowId);
                await collection.ReplaceOneAsync(x => x.Id == objId, item);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
