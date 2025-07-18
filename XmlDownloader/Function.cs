using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using XmlDownloader.Models;
using XmlDownloader.Services;

namespace XmlDownloader
{
    public class Function
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly AppDbContext _db;

        public Function(ILoggerFactory loggerFactory, IConfiguration config, AppDbContext db) {
            _logger = loggerFactory.CreateLogger<Function>();
            _config = config;
            _db = db;
        }

        [Function("XmlDownloader")]
        public async Task Run([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer) {

            _logger.LogInformation($"Function triggered at {DateTime.UtcNow}");

            string? url = _config["XmlApi:Url"];

            if (string.IsNullOrWhiteSpace(url)) {
                _logger.LogError("Missing Xml:Url in configuration.");               
                return;
            }

            var fetcher = new XmlFetcher();
            var xml = await fetcher.GetXmlAsync(url);

            if (xml == null) {
                _logger.LogWarning("Failed to fetch XML data.");
                _db.XmlRecords.Add(new XmlData {
                    JsonData = "\"Xml:URL offline\"",                    
                    Timestamp = DateTime.UtcNow,
                    IsOnline = false,
                });
                await _db.SaveChangesAsync();
                return;
            }

            string? json = fetcher.ConvertToJson(xml);
            if (json == null) {
                _logger.LogError("XML conversion to JSON failed.");
                _db.XmlRecords.Add(new XmlData {
                    JsonData = null,
                    Timestamp = DateTime.UtcNow,
                    IsOnline = true
                });
                await _db.SaveChangesAsync();
                return;
            }

            var record = new XmlData {
                JsonData = json,
                Timestamp = DateTime.UtcNow,
                IsOnline = true,
            };

            _db.XmlRecords.Add(record);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Xml (converted to json data) saved to database.");
        }
    }
}
