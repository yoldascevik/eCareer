using System;
using System.Security.Cryptography;
using System.Text;
using AspectCore;
using MessagePack;

namespace Career.Cache.Helpers
{
    internal static class CacheHelper
    {
        public static string GetCacheKey(string cacheName)
        {
            return $"{cacheName}_*";
        }
        
        public static string GetCacheKey(MethodExecutionArgs args, string cacheName = null)
        {
            string cacheNamePattern = string.IsNullOrEmpty(cacheName)
                ? $"{args.Method.DeclaringType?.Name}.{args.Method.Name}_{{0}}"
                : $"{cacheName}_{{0}}";

            string parametersHash = CalculateObjectMd5Hash(args.Arguments);
            return string.Format(cacheNamePattern, parametersHash ?? "Empty");
        }
        
        public static string GetCacheKey(Type targetType, string targetMethodName = null)
        {
            if (targetType == null)
                throw new ArgumentNullException(nameof(targetType));

            if (!string.IsNullOrEmpty(targetMethodName))
            {
                var methodInfo = targetType.GetMethod(targetMethodName);
                if (methodInfo == null)
                    throw new ArgumentException($"{targetMethodName} mehthod not found in type: {targetType.FullName}");

                return $"{targetType.Name}.{methodInfo.Name}_*";
            }
            
            return $"{targetType.Name}.*";
        }
        
        private static string CalculateObjectMd5Hash(params object[] obj)
        {
            if (obj.Length == 0)
                return null;
            
            byte[] serializedDataByteArray = MessagePackSerializer.Serialize(obj, MessagePack.Resolvers.ContractlessStandardResolver.Options);
            using var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(serializedDataByteArray);
            StringBuilder sb = new StringBuilder();
            foreach (byte h in hash)
                sb.Append(h.ToString("x2"));

            return sb.ToString();
        }
    }
}