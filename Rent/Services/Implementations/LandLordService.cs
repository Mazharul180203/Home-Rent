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
        using var transaction = await _context.Database.BeginTransactionAsync();
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

            var uploadFiles = new List<string>();
            
            foreach (var file in data.filePath)
            {
                if (file == null || file.Length == 0) throw new ArgumentNullException("File is null or empty");
                
                var permissionExtensions = new [] {".pdf",".docx", ".xlsx", ".png", ".jpg", ".jpeg"};
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(ext) || !permissionExtensions.Contains(ext))
                {
                    throw new ArgumentException("File is not a valid file format");
                }
                
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory().ToString(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}{ext}";
                var originalPath = Path.Combine("uploads", uniqueFileName);
                var physicalPath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                
                var addUnits = new unit
                {
                    building_id = data.building_id,
                    unit_number = data.unit_number,
                    status = data.status,
                    rent_amount = data.rent_amount,
                    description = data.description,
                    square_feet = data.square_feet,
                    bedrooms = data.bedrooms,
                    bathrooms = data.bathrooms,
                    filePath = originalPath,
                    fileName = file.FileName,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };
                await _context.units.AddAsync(addUnits);
            }
        
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new CommonResponseDto
            {
                Status = "success",
                Message = "Unit added successfully",
                Data = "success"
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