

namespace PageMetrics.PersistentDataStore
{
    //public class RedisClient
    //{
    //    private string RedisServerUri { get; set; }

    //    public RedisClient(string redisServerUri = null)
    //    {
    //        RedisServerUri = !string.IsNullOrEmpty(redisServerUri) ? redisServerUri : ConfigurationManager.AppSettings["Redis_Server_Uri"];
    //    }

    //    private ConnectionMultiplexer ConnectionManager()
    //    {
    //        ConnectionMultiplexer redis = null;

    //        try
    //        {
    //            redis = ConnectionMultiplexer.Connect(RedisServerUri);
                
    //        }
    //        catch (Exception)
    //        {

    //            throw;
    //        }

    //        return redis;
    //    }

    //    public IDatabase Instance(int db = 0)
    //    {
    //        var instance = ConnectionManager();
            
    //        return db != 0 ? instance.GetDatabase(db) : instance.GetDatabase();
    //    }

    //    public ISubscriber AsyncInstance()
    //    {
    //        var instance = ConnectionManager();
    //        return instance.GetSubscriber();
    //    }
    //}
}
