using MapsterMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Shared.Utilities.General
{
    public interface IServiceBase<T> where T : class
    {
        ILogger<T> GetLogger();
        IMapper GetMapper();
        Guid GuidGenerator();
        void LoggerInfo(string? message, params object?[] args);
        void LoggerError(Exception ex, string? message, params object?[] args);
        void LoggerError(string? message, params object?[] args);

        // Add other common dependencies here if needed, like database context or configuration
    }
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {

        private readonly IMapper _mapper;
        private readonly ILogger<T> _logger;

        public ServiceBase(ILogger<T> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public ILogger<T> GetLogger()
        {
            return _logger;
        }
        public void LoggerInfo(string? message, params object?[] args)
        {
            _logger.LogInformation(message, args);
        }
        public void LoggerError(Exception ex, string? message, params object?[] args)
        {
            _logger.LogError(ex, message, args);
        }
        public void LoggerError(string? message, params object?[] args)
        {
            _logger.LogError(message, args);
        }

        public IMapper GetMapper()
        {
            return _mapper;
        }

        public Guid GuidGenerator()
        {
            return Guid.NewGuid();
        }
    }
}
