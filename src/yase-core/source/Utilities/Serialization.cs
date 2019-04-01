using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace yase_core.Utilities
{
    public enum DateTimeSerializationConverterType
    {
        ISO,
        JavaScript
    }

    public static class SerializationExtensions
    {
        public static string ToJson(this object obj, DateTimeSerializationConverterType dateTimeConverterType = DateTimeSerializationConverterType.ISO, bool ignoreNull = false)
        {
           return ConvertToJson(obj, dateTimeConverterType, ignoreNull,false);
        }

        public static string ToJsonLowercase(this object obj, DateTimeSerializationConverterType dateTimeConverterType = DateTimeSerializationConverterType.ISO, bool ignoreNull = false)
        {
            return ConvertToJson(obj, dateTimeConverterType, ignoreNull, true);
        }

        private static string ConvertToJson(this object obj, DateTimeSerializationConverterType dateTimeConverterType , bool ignoreNull, bool lowerCase )
        {
            var ser = new JsonSerializer();

            if (ignoreNull)
            {
                ser.NullValueHandling = NullValueHandling.Ignore;
            }
            if (lowerCase)
            {
                ser.ContractResolver = new LowercaseContractResolver();
            }
            switch (dateTimeConverterType)
            {
                case DateTimeSerializationConverterType.ISO:
                    ser.Converters.Add(new IsoDateTimeConverter());
                    break;
                case DateTimeSerializationConverterType.JavaScript:
                    ser.Converters.Add(new JavaScriptDateTimeConverter());
                    break;
            }
            using (var tw = new StringWriter())
            {
                ser.Serialize(tw, obj);
                return tw.ToString();
            }
        }

        public static T FromJson<T>(this string content, DateTimeSerializationConverterType dateTimeConverterType = DateTimeSerializationConverterType.ISO)
        {
            var ser = new JsonSerializer();
            switch (dateTimeConverterType)
            {
                case DateTimeSerializationConverterType.ISO:
                    ser.Converters.Add(new IsoDateTimeConverter());
                    break;
                case DateTimeSerializationConverterType.JavaScript:
                    ser.Converters.Add(new JavaScriptDateTimeConverter());
                    break;
            }

            using (var tr = new StringReader(content))
            {
                var result = ser.Deserialize(tr, typeof(T));
                return (T)result;
            }
        }
    }

    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower(CultureInfo.InvariantCulture);
        }
    }
}