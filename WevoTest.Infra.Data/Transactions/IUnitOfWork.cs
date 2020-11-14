namespace WevoTest.Infra.Data.Transactions
{
    public interface IUnitOfWork
    {
        bool Commit();
        void Rollback();
    }
}
