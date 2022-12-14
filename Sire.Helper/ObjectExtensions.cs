using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Reflection;
using Newtonsoft.Json;

namespace Sire.Helper
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ShapeData<TSource>(this TSource source,
            string fields)
        {
            if (source == null) throw new ArgumentNullException("source");

            var dataShapedObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                // all public properties should be in the ExpandoObject 
                var propertyInfos = typeof(TSource)
                    .GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                foreach (var propertyInfo in propertyInfos)
                {
                    // get the value of the property on the source object
                    var propertyValue = propertyInfo.GetValue(source);

                    // add the field to the ExpandoObject
                    ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
                }

                return dataShapedObject;
            }

            // the field are separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                // trim each field, as it might contain leading 
                // or trailing spaces. Can't trim the var in foreach,
                // so use another var.
                var propertyName = field.Trim();

                // use reflection to get the property on the source object
                // we need to include public and instance, b/c specifying a binding flag overwrites the
                // already-existing binding flags.
                var propertyInfo = typeof(TSource)
                    .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                    throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");

                // get the value of the property on the source object
                var propertyValue = propertyInfo.GetValue(source);

                // add the field to the ExpandoObject
                ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
            }

            // return
            return dataShapedObject;
        }

        public static string ToJsonString<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });
        }

        public static T ToObject<T>(this string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public static string GetDescription(this Enum value)
        {
            if (value == null) return "";

            var fiedInfo = value.GetType().GetField(value.ToString());

            if (fiedInfo == null) return "";

            var attributes = (DescriptionAttribute[])fiedInfo.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }


        public static bool CheckMathOperator(this Operator value)
        {
            return value == Operator.Plus ||
               value == Operator.Minus ||
               value == Operator.Divide ||
                value == Operator.SquareRoot ||
                value == Operator.Power ||
               value == Operator.Multiplication ||
               value == Operator.Percentage;
        }
    }
}