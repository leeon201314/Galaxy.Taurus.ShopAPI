using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace Galaxy.Taurus.AuthorizationAPI.GalaxyAuth
{
    /// <summary>
    /// RSA工具类
    /// </summary>
    public class AuthRSAUtils
    {
        private static AuthRSAUtils _singleton = null;
        private static object _lockObj = new object();

        /// <summary>
        /// 钥匙保存的路径
        /// </summary>
        private readonly string _basePath = System.AppDomain.CurrentDomain.BaseDirectory + "AuthKey";

        /// <summary>
        /// 单例
        /// </summary>
        public static AuthRSAUtils Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    lock (_lockObj)
                    {
                        if (_singleton == null)
                        {
                            _singleton = new AuthRSAUtils();
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
        /// 秘钥
        /// </summary>
        public RSAParameters PrivateKeys { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        private AuthRSAUtils()
        {
            string fullPath = Path.Combine(_basePath, "key.private.json");

            if (File.Exists(fullPath))
            {
                PrivateKeys = LoadFromFile(RSAKeyType.PrivateKey);
                PublicKeys = LoadFromFile(RSAKeyType.PublicKey);
            }
            else
                CreateAndSaveKey();
        }

        /// <summary>
        /// 创建并保存钥匙
        /// </summary>
        private void CreateAndSaveKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    PrivateKeys = rsa.ExportParameters(true);
                    PublicKeys = rsa.ExportParameters(false);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }

            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }

            File.WriteAllText(Path.Combine(_basePath, "key.private.json"), JsonConvert.SerializeObject(PrivateKeys));
            File.WriteAllText(Path.Combine(_basePath, "key.public.json"), JsonConvert.SerializeObject(PublicKeys));
        }

        /// <summary>
        /// 从本地文件中读取用来签发 Token 的 RSA Key
        /// </summary>
        /// <param name="keyType">钥匙类型：公钥 私钥</param>
        /// <returns>钥匙参数</returns>
        private RSAParameters LoadFromFile(RSAKeyType keyType)
        {
            string fileName = keyType == RSAKeyType.PrivateKey ? "key.private.json" : "key.public.json";
            string fullPath = Path.Combine(_basePath, fileName);
            return JsonConvert.DeserializeObject<RSAParameters>(File.ReadAllText(fullPath));
        }
    }

    /// <summary>
    /// 钥匙类型
    /// </summary>
    internal enum RSAKeyType
    {
        /// <summary>
        /// 公钥
        /// </summary>
        PublicKey,

        /// <summary>
        /// 秘钥
        /// </summary>
        PrivateKey
    }
}
