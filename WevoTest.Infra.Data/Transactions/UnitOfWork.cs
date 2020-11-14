using System;
using System.Threading.Tasks;
using WevoTest.Infra.Data.Context;

namespace WevoTest.Infra.Data.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WevoTestContext _context;

        public UnitOfWork(WevoTestContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            bool success = (_context.SaveChanges()) > 0;
            return success;
        }

        public void Rollback()
        {
               
        }                                   
    }
}
