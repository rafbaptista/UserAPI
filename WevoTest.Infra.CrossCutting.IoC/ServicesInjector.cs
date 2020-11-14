using Microsoft.Extensions.DependencyInjection;
using WevoTest.Application.Services;
using WevoTest.Domain.Interfaces.Repositories;
using WevoTest.Domain.Interfaces.Services;
using WevoTest.Domain.Services;
using WevoTest.Infra.Data.Context;
using WevoTest.Infra.Data.Repositories;
using WevoTest.Infra.Data.Transactions;

namespace WevoTest.Infra.CrossCutting.IoC
{
    public static class ServicesInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Database
            services.AddScoped<WevoTestContext, WevoTestContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped<IUserService, UserService>();

            //Repositories
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();                         
        }
    }
}
