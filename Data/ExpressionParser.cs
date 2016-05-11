using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ExpressionParser
    {
        public static string ParseExpression<T>(Expression<Func<T, bool>> exp) where T : BizObject
        {
            string expBody = ((LambdaExpression)exp).Body.ToString();
            var info = BizObjectInfoCache.GetOrBuild(typeof(T));

            foreach (var p in exp.Parameters)
            {
                string fieldName=string.Empty;
                info.FieldPropertyNames.TryGetValue(p.Name,out fieldName);
                if (String.IsNullOrEmpty(fieldName))
                {
                    expBody.Replace(p.Name + ".", fieldName);
                }
            }
            expBody.Replace("==", "=");
            expBody.Replace("&&", "AND");
            expBody.Replace("||", "OR");
            return expBody;
        }
    }
}
