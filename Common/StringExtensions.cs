using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public static class StringExtensions
    {
        public static int IndexOf(this string text, string token, int startIndex, bool includeToken, bool retrunLengthIfNotFound)
        {
            var idx = text.IndexOf(token, startIndex);
            if (retrunLengthIfNotFound && idx <= 0)
            {
                return text.Length;
            }
            else
            {
                if (includeToken)
                {
                    return idx + token.Length;
                }
                return idx;
            }
        }
        public static int IndexOf(this string text, char token, int startIndex,bool includeToken, bool retrunLengthIfNotFound)
        {
            var idx = text.IndexOf(token, startIndex);
            if (retrunLengthIfNotFound && idx <= 0)
            {
                return text.Length;
            }
            else
            {
                if (includeToken)
                {
                    return idx + 1;
                }
                return idx;
            }
        }
        public static string SanitizeFileName(this string fileName)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(fileName, "");
        }

        public static string StringBetween(this string strSource, string strBegin, string strEnd, bool includeStart = false, bool includeEnd = false)
        {
            if (string.IsNullOrEmpty(strSource.Trim())) return "";
            int start = strSource.IndexOf(strBegin);
            if (!includeStart)
                start += strBegin.Length;
            if (start != -1)
            {
                int end = strSource.IndexOf(strEnd, start);
                if (end != -1)
                {
                    if (includeEnd)
                    {
                        end += strEnd.Length;
                    }
                    return strSource.Substring(start, end - start);
                }
            }
            return string.Empty;
        }

        public static string ToClassCase(this string text)
        {
            var result = "";
            var txt = text.Replace("_", " ").Replace("-", " ");
            var parts = txt.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var part in parts)
            {
                if (part.Length > 1)
                {
                    result += part.Substring(0, 1).ToUpper() + part.Substring(1).ToLower();
                }
                else
                {
                    result += part.ToUpper();
                }
            }
            return result;
        }

        public static string ToVarCase(this string text)
        {
            var txt = ToClassCase(text);
            if (txt.Length > 1)
            {
                txt = txt.Substring(0, 1).ToLower() + txt.Substring(1);
            }
            else
            {
                txt = txt.ToLower();
            }
            return txt;
        }
    }
}
