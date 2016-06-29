using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using uBlog.Data.DTOs;
using uBlog.IRepository;
using uBlog.Repository.Services;

namespace uBlog.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private UBlogContext uBlogContext;

        public BaseRepository(UBlogContext uBlogContext = null)
        {
            this.uBlogContext = uBlogContext ?? new UBlogContext();
        }

        #region Get
        public async Task<GetOneResult<TEntity>> GetOne<TEntity>(string id) where TEntity : class, new()
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            return await GetOne<TEntity>(filter);
        }

        public async Task<GetOneResult<TEntity>> GetOne<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var res = new GetOneResult<TEntity>();
            try
            {
                var collection = GetCollection<TEntity>();
                var entity = await collection.Find(filter).SingleOrDefaultAsync();
                if (entity != null)
                {
                    res.Entity = entity;
                }
                res.Success = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = HelperService.NotifyException("GetOne", "Exception getting one " + typeof(TEntity).Name, ex);
                return res;
            }
        }

        public async Task<GetManyResult<TEntity>> GetMany<TEntity>(IEnumerable<string> ids) where TEntity : class, new()
        {
            try
            {
                var collection = GetCollection<TEntity>();
                var filter = Builders<TEntity>.Filter.Eq("Id", ids);
                return await GetMany<TEntity>(filter);
            }
            catch (Exception ex)
            {
                var res = new GetManyResult<TEntity>();
                res.Message = HelperService.NotifyException("GetMany", "Exception getting many " + typeof(TEntity).Name + "s", ex);
                return res;
            }
        }

        public async Task<GetManyResult<TEntity>> GetMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var res = new GetManyResult<TEntity>();
            try
            {
                var collection = GetCollection<TEntity>();
                var entities = await collection.Find(filter).ToListAsync();
                if (entities != null)
                {
                    res.Entities = entities;
                }
                res.Success = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = HelperService.NotifyException("GetMany", "Exceptiion getting many " + typeof(TEntity).Name + "s", ex);
                return res;
            }
        }

        public IFindFluent<TEntity, TEntity> FindCursor<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var res = new GetManyResult<TEntity>();

            var collection = GetCollection<TEntity>();
            var cursor = collection.Find(filter);
            return cursor;
        }

        public async Task<GetManyResult<TEntity>> GetAll<TEntity>() where TEntity : class, new()
        {
            var res = new GetManyResult<TEntity>();
            try
            {
                var collection = GetCollection<TEntity>();
                var entities = await collection.Find(new BsonDocument()).ToListAsync();
                if (entities != null)
                {
                    res.Entities = entities;
                }
                res.Success = true;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = HelperService.NotifyException("GetAll", "Exception getting all " + typeof(TEntity).Name + "s", ex);
                return res;
            }
        }

        public async Task<bool> Exists<TEntity>(string id) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = new BsonDocument("Id", id);
            var cursor = collection.Find(query);
            var count = await cursor.CountAsync();

            return (count > 0);
        }

        public async Task<long> Count<TEntity>(string id) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await Count<TEntity>(filter);
        }

        public async Task<long> Count<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var cursor = collection.Find(filter);
            var count = await cursor.CountAsync();
            return count;
        }
        #endregion

        #region Add
        public async Task<Result> AddOne<TEntity>(TEntity item) where TEntity : class, new()
        {
            var res = new Result();
            try
            {
                var collection = GetCollection<TEntity>();
                await collection.InsertOneAsync(item);
                res.Success = true;
                res.Message = "OK";
                return res;
            }
            catch (Exception ex)
            {
                res.Message = HelperService.NotifyException("AddOne", "Exception adding one " + typeof(TEntity).Name, ex);
                return res;
            }


        }
        #endregion

        #region Delete
        public async Task<Result> DeleteOne<TEntity>(string id) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await DeleteOne<TEntity>(filter);
        }

        public async Task<Result> DeleteOne<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var result = new Result();

            try
            {
                var collection = GetCollection<TEntity>();
                var deleteRes = await collection.DeleteOneAsync(filter);
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = HelperService.NotifyException("DeleteOne", "Exception deleting one " + typeof(TEntity).Name, ex);
                return result;
            }
        }

        public async Task<Result> DeleteMany<TEntity>(IEnumerable<string> ids) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().In("Id", ids);
            return await DeleteMany<TEntity>(filter);
        }

        public async Task<Result> DeleteMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var result = new Result();
            try
            {
                var collection = GetCollection<TEntity>();
                var deleteRes = await collection.DeleteManyAsync(filter);
                if (deleteRes.DeletedCount < 1)
                {
                    var ex = new Exception();
                    result.Message = HelperService.NotifyException("DeleteMany", "Some " + typeof(TEntity).Name + "s could not be deleted.", ex);
                    return result;
                }
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = HelperService.NotifyException("DeleteMany", "Some " + typeof(TEntity).Name + "s could not be deleted.", ex);
                return result;
            }
        }
        #endregion

        #region Update
        public async Task<Result> UpdateOne<TEntity>(string id, UpdateDefinition<TEntity> update) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await UpdateOne<TEntity>(filter, update);
        }

        public async Task<Result> UpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : class, new()
        {
            var result = new Result();
            try
            {
                var collection = GetCollection<TEntity>();
                var updateRes = await collection.UpdateOneAsync(filter, update);
                if (updateRes.ModifiedCount < 1)
                {
                    var ex = new Exception();
                    result.Message = HelperService.NotifyException("UpdateOne", "ERROR: updateRes.ModifiedCount < 1 for entity: " + typeof(TEntity).Name, ex);
                    return result;
                }
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = HelperService.NotifyException("UpdateOne", "Exception updating entity: " + typeof(TEntity).Name, ex);
                return result;
            }
        }

        public async Task<Result> UpdateMany<TEntity>(IEnumerable<string> ids, UpdateDefinition<TEntity> update) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().In("Id", ids);
            return await UpdateOne<TEntity>(filter, update);

        }

        public async Task<Result> UpdateMany<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : class, new()
        {
            var result = new Result();
            try
            {
                var collection = GetCollection<TEntity>();
                var updateRes = await collection.UpdateManyAsync(filter, update);
                if (updateRes.ModifiedCount < 1)
                {
                    var ex = new Exception();
                    result.Message = HelperService.NotifyException("UpdateMany", "ERROR: updateRes.ModifiedCount < 1 for entities: " + typeof(TEntity).Name + "s", ex);
                    return result;
                }
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = HelperService.NotifyException("UpdateMany", "Exception updating entities: " + typeof(TEntity).Name + "s", ex);
                return result;
            }
        }
        #endregion

        #region Find And Update
        public async Task<GetOneResult<TEntity>> GetAndUpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options) where TEntity : class, new()
        {
            var result = new GetOneResult<TEntity>();
            try
            {
                var collection = GetCollection<TEntity>();
                result.Entity = await collection.FindOneAndUpdateAsync(filter, update, options);
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = HelperService.NotifyException("GetAndUpdateOne", "Exception getting and updating entity: " + typeof(TEntity).Name, ex);
                return result;
            }
        }
        #endregion


        private IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return uBlogContext.GetCollection<TEntity>();
        }
    }
}
