using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using Reto.Domain.Exceptions;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;

namespace Reto.API.Converters
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "dd/MM/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && reader.TryGetDateTime(out DateTime date))
            {
                return date;
            }

            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out long epoch))
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(epoch).DateTime;
            }

            string dateString = reader.GetString();
            if (DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            throw new ValidationException($"El formato de fecha debe ser '{DateFormat}'");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }
}
