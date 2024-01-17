using Housing.API.Data.Interfaces;
using Housing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Housing.API.Data.Repo
{
    public class PropertyTypeRepository : IPropertTypeRepository
    {
        private readonly DataContext _context;

        public PropertyTypeRepository(DataContext context)
        {
           _context = context;
        }

        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
            return await _context.FurnishingTypes.ToListAsync();
        }

        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync()
        {
            return await _context.PropertyTypes.ToListAsync();
        }
    }
}
