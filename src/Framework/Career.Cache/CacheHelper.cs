using System.Security.Cryptography;
using System.Text;
using AspectCore;
using MessagePack;

namespace Career.Cache
{
    internal static class CacheHelper
    {
        public static string GetCacheKey(MethodExecutionArgs args, string cacheNamePrefix = null)
        {
            string cacheNamePattern = string.IsNullOrEmpty(cacheNamePrefix)
                ? $"{args.Method.DeclaringType?.Name}.{args.Method.Name}_{{0}}"
                : $"{cacheNamePrefix}.{args.Method.DeclaringType?.Name}.{args.Method.Name}_{{0}}";

            string parametersHash = CalculateObjectMd5Hash(args.Arguments);
            return string.Format(cacheNamePattern, parametersHash);
        }
        
        private static string CalculateObjectMd5Hash(params object[] obj)
        {
            byte[] serializedDataByteArray = MessagePackSerializer.Serialize(obj, MessagePack.Resolvers.ContractlessStandardResolver.Options);
            using (var md5 = MD5.Create()) {
                byte[] hash = md5.ComputeHash(serializedDataByteArray);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                    sb.Append(hash[i].ToString("x2"));
                
                return sb.ToString();
            }
        }
    }
}