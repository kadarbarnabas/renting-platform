using RentingPlatform.Shared;

namespace RentingPlatform.UI.Services;

public interface ILoginService
{
    Task<string> LoginUserAsync(Users loginUsers);
}