using System;
using WevoTest.Domain.Entities;
using WevoTest.Domain.Interfaces.Repositories;
using WevoTest.Domain.Interfaces.Services;
using WevoTest.Domain.Services;

namespace WevoTest.Application.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
            : base(repository)
        {
            _repository = repository;
        }       

    }
}
