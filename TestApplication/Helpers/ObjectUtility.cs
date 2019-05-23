using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TestApplication.Helpers
{
    public static class ObjectUtility
    {
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            return property != null ? property.GetValue(obj, null) : null;
        }

        public static IEnumerable<string> GetAllColumnsByObject<T>()
        {
            return typeof(T).GetProperties().Where(p => !(typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType) || p.PropertyType.Namespace == typeof(T).Namespace)
                                                    || p.PropertyType == typeof(string)).Select(x => x.Name);
        }

        public static EntityObject LoadAllChild(this EntityObject source)
        {
            List<PropertyInfo> PropList = (from a in source.GetType().GetProperties()
                                           where a.PropertyType.Name == "EntityCollection`1"
                                           select a).ToList();
            foreach (PropertyInfo prop in PropList)
            {
                object instance = prop.GetValue(source, null);
                var isLoad = (bool)instance.GetType().GetProperty("IsLoaded").GetValue(instance, null);
                if (!isLoad)
                {
                    MethodInfo mi = (from a in instance.GetType().GetMethods()
                                     where a.Name == "Load" && a.GetParameters().Length == 0
                                     select a).FirstOrDefault();

                    mi.Invoke(instance, null);
                }
            }
            return source;
        }
    }
}