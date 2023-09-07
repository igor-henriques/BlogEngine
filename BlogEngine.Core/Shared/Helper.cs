using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogEngine.Core.Shared;

public static class Helper
{
    public static bool CheckForPasswordRequiredCharacters(string password)
    {
        var hasNumber = password.Any(char.IsDigit);
        var hasUppercase = password.Any(char.IsUpper);
        var hasLowercase = password.Any(char.IsLower);
        var hasSpecialCharacter = CompiledRegex.CheckSpecialCharacterRegex().IsMatch(password);

        return hasNumber && hasUppercase && hasLowercase && hasSpecialCharacter;
    }

    public static Dictionary<string, object> DecodeJwtPayload(string encodedPayload)
    {
        byte[] payloadBytes = Base64UrlEncoder.DecodeBytes(encodedPayload);
        string payloadJson = Encoding.UTF8.GetString(payloadBytes);

        return JsonConvert.DeserializeObject<Dictionary<string, object>>(payloadJson);
    }
}
