using Housing.API.Models;

namespace Housing.API.Data.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent);
        Task<Property> GetPropertyDetailsAsync(int id);
        Task<Property> GetPropertyByIdAsync(int id);
        void AddPropertyAsync(Property property);
        void DeleteProperty(int id);

    }
}
