using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PieFunds.Application.Features.Users.Queries.GetUserByEmail;
using PieFunds.Application.Interfaces;
using PieFunds.Domain.Entities;
using PieFunds.Infrastructure.Persistence;

namespace PieFunds.Tests.TestInfrastructure
{
    public class MediatorFixture : IDisposable
    {

        private readonly ServiceProvider _provider;

        public MediatorFixture()
        {
            var services = new ServiceCollection();

            // MediatR registration: scan Application assembl
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetUserByEmailQuery).Assembly));

            // InMemory repository for demo
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();

            // Add logging support
            // Add the logging services manually since AddLogging extension is missing
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));

            _provider = services.BuildServiceProvider();
            Mediator = _provider.GetRequiredService<IMediator>();
        }

        public IMediator Mediator { get; }

        public IServiceProvider ServiceProvider => _provider;

        public void Dispose()
        {
            _provider.Dispose();
        }

    }
}
