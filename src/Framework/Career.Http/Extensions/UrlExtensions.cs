using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.WebUtilities;

namespace Career.Http.Extensions;

public static class UrlExtensions
{
    public static string GetUrlWithQueryObject(string url, object queryParamObj)
    {
        url = url == null ? string.Empty : url.TrimEnd('/');

        if (queryParamObj == null)
            throw new ArgumentNullException(nameof(queryParamObj));

        Type queryParamObjType = queryParamObj.GetType();
        if (queryParamObjType.IsValueType || queryParamObjType == typeof(string))
        {
            if (string.IsNullOrEmpty(url))
                return queryParamObj.ToString();

            return $"{url}/{queryParamObj}";
        }

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