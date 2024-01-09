using Microsoft.Extensions.Hosting;
using MiddlewareBinding;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(c=>
    {
        c.UseMiddleware<ServiceBusMessageMiddleware>();
    })
    .Build();

host.Run();
