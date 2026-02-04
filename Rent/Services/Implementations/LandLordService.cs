using Data.DBContexts;
using Data.Dtos;
using Data.Models;
using Services.Interfaces;

namespace Services.Implementations;

public class LandLordService :ILandLordService
{
    private readonly AppDBContext _context;
    
    public LandLordService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<CommonResponseDto> CreatePropertiesService(propetiesDto data, string UserIDClaims)
    {
        try
        {
            var properties = new property
            {
                owner_id = long.Parse(UserIDClaims),
                name = data.name,
                address = data.address,
                type = data.type,
                units_count = data.units_count,
                amenities = data.amenities,
                location_geo = data.location_geo,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow

            };
        
            await _context.properties.AddAsync(properties);
            await _context.SaveChangesAsync();
            return new CommonResponseDto
            {
                Status = "success",
                Message = "User registered successfully",
                Data = null
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
    
    public async Task<CommonResponseDto> GetPropertyService(long id)
    {
        var property = await _context.properties.FindAsync(id);
        if (property == null)
        {
            return new CommonResponseDto
            {
                Status = "fail",
                Message = "Property not found",
                Data = null
            };
        }

        return new CommonResponseDto
        {
            Status = "success",
            Message = "Property registered successfully",
            Data = property
        };
    }
}