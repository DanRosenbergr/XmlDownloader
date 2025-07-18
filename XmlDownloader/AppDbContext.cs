
using Microsoft.EntityFrameworkCore;
using XmlDownloader.Models;

namespace XmlDownloader {

    public class AppDbContext : DbContext {
               
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        public DbSet<XmlData> XmlRecords { get; set; }
    }
}

