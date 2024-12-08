namespace RestaurantReservationWebAPI.TokenGenerators
{
    public interface ITokenGenerator
    {
        string GenerateToken(string username, string password);
        bool ValidateToken(string token);
    }
}
