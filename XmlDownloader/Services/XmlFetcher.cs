using System.Xml.Linq;
using Newtonsoft.Json;

namespace XmlDownloader.Services {

    public class XmlFetcher {

        private readonly HttpClient _httpClient = new();

        public async Task<string?> GetXmlAsync(string url) {
            try { 
                return await _httpClient.GetStringAsync(url); 
            } 
            catch { 
                return null; 
            }
        }

        public string ConvertToJson(string xml) {
            try {
                var doc = XDocument.Parse(xml);
                return JsonConvert.SerializeXNode(doc, Newtonsoft.Json.Formatting.Indented);
            }
            catch {
                return null;
            }
        }
    }
}
