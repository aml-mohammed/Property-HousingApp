using Microsoft.AspNetCore.Components.Web;

namespace Housing.API.Data.Interfaces
{
    public interface IUnitOfWork
    {
        public ICityRepository CityRepository { get; }
        public IUserRepository UserRepository { get; }
        public IPropertyRepository PropertyRepository { get; }
        public IPropertTypeRepository PropertTypeRepository { get; }

        Task<bool> SaveAsync();

    }
}
