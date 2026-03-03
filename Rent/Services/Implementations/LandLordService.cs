using Data.DBContexts;
using Data.Dtos;
using Data.Models;
using Microsoft.EntityFrameworkCore;
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

    public async Task<CommonResponseDto> CreateUnitsService(long UserID, unitDto data)
    {
        try
        {
            List<property> properties = await _context.properties
                .Where(u => u.owner_id == UserID)
                .ToListAsync();

            int cnt = 0;
            if (properties.Count > 0)
            {
                foreach (var property in properties)
                {
                    if (property.id == data.building_id && property.owner_id == UserID)
                    {
                        cnt++;
                        break;
                    }
                } 
            }
            
            if (cnt == 0)
            {
                return new CommonResponseDto
                {
                    Status = "fail",
                    Message = "No properties found for user for that building",
                    Data = null
                };
            }

            var units = new unitDto
            {
                building_id = data.building_id,
                unit_number = data.unit_number,
                status = data.status,
                rent_amount = data.rent_amount,
                description = data.description,
                square_feet = data.square_feet,
                bedrooms = data.bedrooms,
                bathrooms = data.bathrooms,
                photos = data.photos,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

            return new CommonResponseDto
            {
                Status = "success",
                Message = "Unit added successfully",
                Data = units
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