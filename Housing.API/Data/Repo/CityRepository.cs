using Housing.API.Data.Interfaces;
using Housing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Housing.API.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;

        public CityRepository(DataContext context)
        {
            _context = context;
        }
        public void AddCity(City city)
        {
            _context.cities.AddAsync(city);
        }

        public  void DeleteCity(int id)
        {
            var city = _context.cities.Find(id);
            _context.cities.Remove(city);
        }

        public async Task<City> FindCity(int id)
            
        {
            return await _context.cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.cities.ToListAsync();
        }

       
    }
}
