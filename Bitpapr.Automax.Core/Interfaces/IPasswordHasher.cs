namespace Bitpapr.Automax.Core.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}