using Data.DBContexts;
using Data.Dtos;
using Services.Interfaces;

namespace Services.Implementations;

public class UserService : IUserService
{
    public readonly AppDBContext _context;

    public UserService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<CommonResponseDto> UpdateProfile(long UserID, ProfileUpdateDto data)
    {
        try
        {
            var userdetails = await _context.users.FindAsync(UserID);

            if (userdetails == null)
            {
                return new CommonResponseDto
                {
                    Status="fail",
                    Message = "User not found",
                    Data= null,
                };
            }

            userdetails.full_name = data.full_name ?? userdetails.full_name;
            userdetails.phone = data.phone ?? userdetails.phone;
            userdetails.screening_score =data.screening_score ?? userdetails.screening_score;
            userdetails.email = data.email ?? userdetails.email;
            userdetails.background_check = data.background_check ?? userdetails.background_check;
            userdetails.profile_pic = data.profile_pic ?? userdetails.profile_pic;

            await _context.SaveChangesAsync();
            return new CommonResponseDto
            {
                Status = "success",
                Message = "User updated",
                Data = data,
            };

        }
        catch(Exception e)
        {
            return new CommonResponseDto
            {
                Status = "error",
                Message = $"An error occurred: {e.Message}",
                Data = null
            };
        }
    }
    
}

