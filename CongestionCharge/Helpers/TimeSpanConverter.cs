using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CongestionCharge.Helpers
{
    // Simple custom TimeSpan converter for deserialization in .NET 5
    // https://github.com/dotnet/runtime/issues/29932#issuecomment-694650687
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
