using Microsoft.Extensions.Logging;
using ILogger = zipkin4net.ILogger;

namespace Lily.Microservice.Microparts.CallChain.Zipkin.Imp.Common
{
    internal class TracingLogger : ILogger
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public TracingLogger(ILoggerFactory loggerFactory, string loggerName)
        {
            _logger = loggerFactory.CreateLogger(loggerName);
        }
        public void LogError(string message)
        {
            _logger.LogError(message);
        }
        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }
        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}