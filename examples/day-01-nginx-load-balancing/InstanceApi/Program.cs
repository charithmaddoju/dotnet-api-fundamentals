var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var instanceName = Environment.GetEnvironmentVariable("INSTANCE_NAME") ?? "unknown";

app.MapGet("/", (HttpContext context) => new
{
    instance = instanceName,
    port = context.Connection.LocalPort,
    time = DateTime.UtcNow.ToString("O"),
});

app.Run();
