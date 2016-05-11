using System;
using System.Collections.Concurrent;

namespace Data
{
    public static class BizObjectInfoCache
    {
        private static readonly ConcurrentDictionary<Type, BizObjectInfo> Cache = new ConcurrentDictionary<Type, BizObjectInfo>();

        public static BizObjectInfo GetOrBuild(Type t,bool onlyGet=false)
        {
            BizObjectInfo data;
            var bi=Cache.TryGetValue(t, out data) ? data : null;
            if (!onlyGet)
            {
                if (bi == null)
                {
                    bi = new BizObjectInfo(t);
                    if (bi != null)
                        Add(t, bi);
                }
            }
            return bi;
        }

        public static void Add(Type t, BizObjectInfo ti)
        {
            Cache.AddOrUpdate(t, ti,(key, item) => 
                {
                    item.FieldPropertyInfo = ti.FieldPropertyInfo;
                    item.FieldPropertyNames = ti.FieldPropertyNames;
                    item.Fields = ti.Fields;
                    item.PrimaryKeys = ti.PrimaryKeys;
                    item.TableName = ti.TableName;
                    item.TableOwner = ti.TableOwner;
                    return item;
                });
        }
    }
}
