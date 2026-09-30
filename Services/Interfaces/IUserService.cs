using Data.Dtos;

namespace Services.Interfaces;

public interface IUserService
{
    Task<CommonResponseDto> UpdateProfile(long UserID, ProfileUpdateDto data);
}