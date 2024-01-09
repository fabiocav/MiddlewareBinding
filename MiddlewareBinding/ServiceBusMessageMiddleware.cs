using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareBinding
{
    internal class ServiceBusMessageMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger<ServiceBusMessageMiddleware> _logger;

        public ServiceBusMessageMiddleware(ILogger<ServiceBusMessageMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var bindingMetadata = context.FunctionDefinition.InputBindings.FirstOrDefault().Value;

            var bindingData = await context.BindInputAsync<ServiceBusReceivedMessage>(bindingMetadata);

            _logger.LogInformation(bindingData.Value?.MessageId);

            await next(context);
        }
    }
}
