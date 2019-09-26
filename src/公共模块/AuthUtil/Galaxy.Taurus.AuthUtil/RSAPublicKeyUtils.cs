using System.IO;
using System.Security.Cryptography;

namespace Galaxy.Taurus.AuthUtil
{
    /// <summary>
    /// RSA工具类
    /// </summary>
    public class RSAPublicKeyUtils
    {
        private static RSAPublicKeyUtils _singleton = null;
        private static object _lockObj = new object();

        /// <summary>
        /// 钥匙保存的路径
        /// </summary>
        private readonly string _basePath = System.AppDomain.CurrentDomain.BaseDirectory + "AuthKey";

        /// <summary>
        /// 单例
        /// </summary>
        public static RSAPublicKeyUtils Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    lock (_lockObj)
                    {
                        if (_singleton == null)
                        {
                            _singleton = new RSAPublicKeyUtils();
                        }
                    }
                }

                return _singleton;
            }
        }

        /// <summary>
        /// 公钥
        /// </summary>
        public RSAParameters PublicKeys { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        private RSAPublicKeyUtils()
        {
            PublicKeys = LoadFromFile();
        }

        /// <summary>
        /// 从本地文件中读取用来签发 Token 的 RSA Key
        /// </summary>
        private RSAParameters LoadFromFile()
        {
            string fullPath = Path.Combine(_basePath, "key.public.json");  
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RSAParameters>(File.ReadAllText(fullPath));
        }
    }
}
