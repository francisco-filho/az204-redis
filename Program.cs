// See https://aka.ms/new-console-template for more information
using System.Data.Common;
using StackExchange.Redis;

namespace Az204Redis {
    class MyRedisClient {
        // istatic string connectionString = "az204cg.redis.cache.windows.net:6380,password=4Edi4LSimlfUWidY94YgdXDE3JptGyyfgAzCaFUCJ98=,ssl=True,abortConnect=False";
        private static string connectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
        public static void Main(string[] args){
            var conn = ConnectionMultiplexer.Connect(connectionString);
            IDatabase db = conn.GetDatabase();

            Console.WriteLine("Are you ok?" + db.Execute("PING"));

            bool keyexists = db.KeyExists("greeting");
            Console.WriteLine("key exists: " + keyexists);

            if (!keyexists){
                Console.WriteLine("Setting the key value");
                db.StringSet("greeting", "hello world");
            }

            keyexists = db.KeyExists("greeting");

            if (keyexists){
                Console.WriteLine("The key now exists");
                Console.WriteLine(db.StringGet("greeting"));
                db.KeyExpire("greeting", DateTime.Now.AddSeconds(15));
            }
        }
    }
}