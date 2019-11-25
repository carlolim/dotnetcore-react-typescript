namespace Quality.Common.Helpers
{
    public interface IClaimsHelper
    {
        int ClientId { get; }
        string FirstName { get; }
        string LastName { get; }
        int UserId { get; }
        bool IsAdmin { get; }
    }
}