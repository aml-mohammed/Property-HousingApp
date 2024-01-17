using Housing.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Housing.API.Data.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        void DeleteCity(int id);
        Task<City> FindCity(int id);
        //void UpdateCity(City city);
      


    }
}
