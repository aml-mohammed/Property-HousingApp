using Housing.API.Data.Interfaces;

namespace Housing.API.Data.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public ICityRepository CityRepository=>
      
                 new CityRepository(_context);
      

        public IUserRepository UserRepository=>
        
                 new UserRepository(_context);

        public IPropertyRepository PropertyRepository =>

            new PropertyRepository(_context);

        public IPropertTypeRepository PropertTypeRepository =>
            new PropertyTypeRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}
