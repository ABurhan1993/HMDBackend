namespace CrmBackend.Domain.Services;

public static class PhoneValidator
{
    public static bool IsValidUAEPhone(string phone)
    {
        return phone.StartsWith("971") && phone.Length == 12;
    }
}
