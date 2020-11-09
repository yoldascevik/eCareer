using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.WebUtilities;

namespace Career.Http.Extensions
{
    public static class UrlExtensions
    {
        public static string GetUrlWithQueryObject(string url, object queryParamObj)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            
            if (queryParamObj == null)
                throw new ArgumentNullException(nameof(queryParamObj));

            var parameters = new Dictionary<string, string>();
            PropertyInfo[] properties = queryParamObj.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                string value = propertyInfo.GetValue(queryParamObj)?.ToString();
                if (string.IsNullOrEmpty(value))
                    continue;
                
                parameters.Add(propertyInfo.Name, value);
            }

            return QueryHelpers.AddQueryString(url, parameters);
        }
    }
}