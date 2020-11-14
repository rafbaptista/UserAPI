using System;
using System.Collections.Generic;
using System.Text;
using WevoTest.Domain.Entities;
using WevoTest.Domain.Interfaces.Repositories;
using WevoTest.Infra.Data.Context;

namespace WevoTest.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {      
        public UserRepository(WevoTestContext context)
            :base(context)
        {
            
        }
    }
}
