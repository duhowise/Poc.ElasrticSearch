using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Serilog.Debugging.SelfLog.Enable(Console.Error);
Log.Information("Starting up!");
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostBuilderContext.Configuration);
        var elasticSearchUrl = hostBuilderContext.Configuration["Serilog:WriteTo:1:Args:nodeUris"];
        //loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchUrl))
        //{
        //    AutoRegisterTemplate = true,
        //    IndexFormat =
        //        $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{hostBuilderContext.HostingEnvironment.EnvironmentName.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
        //    NumberOfReplicas = 1,
        //    NumberOfShards = 2,
        //    ModifyConnectionSettings = x => x.BasicAuthentication(hostBuilderContext.Configuration["ElasticConfiguration:Username"], hostBuilderContext.Configuration["ElasticConfiguration:Password"])

        //});
    });

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    app.UseSerilogRequestLogging();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "An unhandled exception occurred during bootstrapping");
}
finally
{
    Log.CloseAndFlush();
}