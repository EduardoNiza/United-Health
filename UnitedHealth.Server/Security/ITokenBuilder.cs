namespace UnitedHealth.Server.Config
{
    public interface ITokenBuilder
    {
        string BuildToken(string id, string profile);
    }
}
