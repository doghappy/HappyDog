using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace HappyDog.Infrastructure.Extensions
{
    /// <summary>
    /// 将时间输出为时间戳，用法"[JsonConverter(typeof(TimestampConvert))]"
    /// </summary>
    public class TimestampConvert : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long ticks = long.Parse(reader.Value.ToString());
            return new DateTime(ticks * 10000000 + 621355968000000000).ToLocalTime();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime datetime)
            {

                long timestamp = (datetime.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                writer.WriteValue(timestamp);
            }
            else
            {
                throw new JsonSerializationException("Expected date object value.");
            }
        }
    }
}
