namespace HR.Core.Application.Interfaces.Contracts
{
    public interface IAccountManager
    {
        public bool LogIn(string userName, string password);
    }
}
