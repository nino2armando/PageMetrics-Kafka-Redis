using System;
using System.Collections.Generic;
using PageMetrics.PersistentDataStore.Models;
using ServiceStack.Redis;

namespace PageMetrics.PersistentDataStore
{
    public class PageRepository : IPageRepository
    {
        private readonly IRedisClient _redisClient;

        public PageRepository(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public IList<PageModel> GetAll()
        {
            var typedClient = _redisClient.As<PageModel>();
            return typedClient.GetAll();
        }

        public PageModel Get(string id)
        {
            var typedClient = _redisClient.As<PageModel>();
            return typedClient.GetById(id);
        }

        public PageModel Store(PageModel page)
        {
            var typedClient = _redisClient.As<PageModel>();

            // todo: we need a way of validation the KEY or ID
            //if (page.Key == default(Guid))
            //{
            //    page.Key = Guid.NewGuid();
            //}
            return typedClient.Store(page);
        }
    }
}
