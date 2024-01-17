using Housing.API.Data.Interfaces;
using Housing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Housing.API.Data.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _context;

        public PropertyRepository(DataContext context)
        {
            _context = context;
        }
        public void AddPropertyAsync(Property property)
        {
            _context.Properties.AddAsync(property);
        }

        public void DeleteProperty(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            return await _context.Properties
                .Include(p=>p.PropertyType)
                .Include(p=>p.FurnishingType)
                 .Include(p => p.Photos)
                .Include(p=>p.City)
                .Where(p=>p.SellRent==sellRent)
                .ToListAsync();
        }

        public async Task<Property> GetPropertyDetailsAsync(int id)
        {
            var properties= await _context.Properties
               .Include(p => p.PropertyType)
               .Include(p => p.FurnishingType)
               .Include(p => p.Photos)
               .Include(p => p.City)
               .Where(p => p.Id == id)
               .FirstAsync();
            return properties;
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            var properties = await _context.Properties
               
               .Include(p => p.Photos)
               .Where(p => p.Id == id)
               .FirstOrDefaultAsync();
            return properties;
        }
    }
}
