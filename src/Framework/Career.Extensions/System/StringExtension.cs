using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Career.Extensions.System;

public static class StringExtension
{
    public static bool IsValidJson(this string text)
    {
        text = text.Trim();
        if ((text.StartsWith("{") && text.EndsWith("}")) ||
            (text.StartsWith("[") && text.EndsWith("]")))
        {
            try
            {
                JsonDocument.Parse(text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static bool IsHtml(this string text)
    {
        Regex tagRegex = new Regex(@"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");
        return tagRegex.IsMatch(text);
    }
}