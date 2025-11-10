using Azure.Messaging.ServiceBus;
using UmbracoAssignment.Services;
using UmbracoAssignment.Workers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();


//submitions
builder.Services.AddScoped<FormSubmissionsService>();

//Servicebus
var sbConn = builder.Configuration["Azure:ServiceBus:ConnectionString"]
    ?? throw new InvalidOperationException("Missing Azure:ServiceBus:ConnectionString");

var queue = builder.Configuration["Azure:ServiceBus:QueueName"]
    ?? throw new InvalidOperationException("Missing Azure:ServiceBus:QueueName");

var acsConn = builder.Configuration["Azure:Communication:Email:ConnectionString"]
    ?? throw new InvalidOperationException("Missing Azure:Communication:Email:ConnectionString");

var fromAddr = builder.Configuration["Azure:Communication:Email:FromAddress"]
    ?? throw new InvalidOperationException("Missing Azure:Communication:Email:FromAddress");

builder.Services.AddSingleton(_ => new ServiceBusClient(sbConn!));
builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<ServiceBusClient>();
    return client.CreateSender(queue!);
});

//worker
builder.Services.AddHostedService<EmailWorker>();


WebApplication app = builder.Build();
await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
