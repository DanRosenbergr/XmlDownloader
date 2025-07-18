using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlDownloader.Models {
    public class XmlData {

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? JsonData { get; set; }
        public bool IsOnline { get; set; } 
    }
}
