using CSRedis;

namespace Galaxy.Taurus.CachedUtil
{
    public class CSRedisInitHelper
    {
        private static string cachedAddress = "127.0.0.1:6379";

        public static void Init(string server, int port)
        {
            CSRedisInitHelper.cachedAddress = $"{server}:{port}";

            //password<空> 密码
            //defaultDatabase 0   默认数据库
            //poolsize    50  连接池大小
            //preheat true    预热连接
            //ssl false   是否开启加密传输
            //writeBuffer 10240   异步方法写入缓冲区大小(字节)
            //tryit   0   执行命令出错，尝试重试的次数
            //name<空> 连接名称，可以使用 Client List 命令查看
            //prefix<空> key前辍，所有方法都会附带此前辍，csredis.Set(prefix + "key", 111);
            CSRedisClient csredis = new CSRedisClient($"{CSRedisInitHelper.cachedAddress},poolsize=50,preheat=true,ssl=false,writeBuffer=10240,tryit=0");
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
        }
    }
}
