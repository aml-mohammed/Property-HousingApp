using Housing.API.Models;

namespace Housing.API.Data.Interfaces
{
    public interface IPropertTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();
        Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync();
    }
}
