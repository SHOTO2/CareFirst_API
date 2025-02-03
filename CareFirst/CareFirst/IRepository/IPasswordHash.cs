namespace CareFirst.IRepository
{
    public interface IPasswordHash
    {
        string Hash(string password);
        bool Verified(string password, string passwordHash);
    }
}
