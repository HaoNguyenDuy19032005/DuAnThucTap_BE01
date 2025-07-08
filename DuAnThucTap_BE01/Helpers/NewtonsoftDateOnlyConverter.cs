using Newtonsoft.Json;
using System;
using System.Globalization;

namespace DuAnThucTap_BE01.Helpers
{
    public class NewtonsoftDateOnlyConverter : JsonConverter<DateOnly?>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly? ReadJson(JsonReader reader, Type objectType, DateOnly? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            var s = reader.Value.ToString();
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            if (DateOnly.TryParseExact(s, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
            {
                return dt;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer)
        {
            if (value.HasValue)
            {
                writer.WriteValue(value.Value.ToString(Format, CultureInfo.InvariantCulture));
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}