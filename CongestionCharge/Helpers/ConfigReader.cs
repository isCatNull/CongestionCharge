using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CongestionCharge.Helpers
{
    public class ConfigReader
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new TimeSpanConverter() }
        };

        public static async Task<DTOs.Config> ReadAsync(string filePath)
        {
            var contents = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<DTOs.Config>(contents, _jsonSerializerOptions);
        }
    }
}
