using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, builder) => 
    {
        var env = context.HostingEnvironment;

        builder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) => {
        string? connectionString = context.Configuration.GetConnectionString("DefaultConnection");        

        services.AddDbContext<XmlDownloader.AppDbContext>(options =>
            options.UseSqlServer(connectionString));
    })
    .Build();

host.Run();
