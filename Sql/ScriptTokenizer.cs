﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Sql
{
    public class ScriptTokenizer
    {
        public List<SqlFragment> TokenizeScript(string sql)
        {
            int endIdx = 0;
            int length = sql.Length;
            var fragments = new List<SqlFragment>();
            var sqlU = sql.ToUpper();
            for (var i = 0; i < sqlU.Length; i++)
            {
                if ((i + 5) < length)
                {
                    var cmd = new string(new char[] { sqlU[i], sqlU[i + 1], sqlU[i + 2], sqlU[i + 3], sqlU[i + 4], sqlU[i + 5] });
                    if (cmd.StartsWith("--", StringComparison.Ordinal))
                    {
                        var sentence = sql.Substring(i, sql.IndexOf(Environment.NewLine, i,false,true) - i).ToUpper();
                        i += sentence.Length;
                    }
                    else if (cmd.StartsWith("ALTER", StringComparison.OrdinalIgnoreCase) || cmd.StartsWith("CREATE", StringComparison.OrdinalIgnoreCase) ||
                             cmd.StartsWith("DROP", StringComparison.OrdinalIgnoreCase) || cmd.StartsWith("GRANT", StringComparison.OrdinalIgnoreCase) ||
                             cmd.StartsWith("REVOKE", StringComparison.OrdinalIgnoreCase) || cmd.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase) ||
                             cmd.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase) || cmd.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) ||
                             cmd.StartsWith("COMMIT", StringComparison.OrdinalIgnoreCase))
                    {

                        if (cmd.StartsWith("GRANT", StringComparison.OrdinalIgnoreCase) || cmd.StartsWith("REVOKE", StringComparison.OrdinalIgnoreCase) ||
                            cmd.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase) || cmd.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase) || 
                            cmd.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase))
                        {
                            endIdx = sql.IndexOf(';', i,true,true);
                            string fragment = sql.Substring(i, endIdx - i);
                            fragments.Add(new SqlFragment(i,endIdx, fragment));
                            i = endIdx;
                        }
                        else {
                            var sentence = sqlU.Substring(i, sql.IndexOf(Environment.NewLine, i,false,true) - i).ToUpper();
                            if (sentence.Contains(" TABLE ") ||
                                sentence.Contains(" VIEW ") ||
                                sentence.Contains(" SEQUENCE ") ||
                                sentence.Contains(" SESSION "))
                            {
                                endIdx = sql.IndexOf(';', i,true,true);
                                string fragment = sql.Substring(i, endIdx - i);
                                fragments.Add(new SqlFragment(i, endIdx, fragment));
                                i = endIdx;
                            }else if (sentence.Contains(" PROCEDURE ") || sentence.Contains(" PACKAGE ") ||
                                sentence.Contains(" PACKAGEBODY ") || sentence.Contains(" TRIGGER "))
                            {
                                endIdx = sql.IndexOf('/', i,true,true);
                                string fragment = sql.Substring(i, endIdx - i);
                                fragments.Add(new SqlFragment(i, endIdx, fragment));
                                i = endIdx;
                            }
                        }
                    }
                }
            }
            return fragments;
        }
    }
}
