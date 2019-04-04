using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelstarCES.Data;
using TelstarCES.Data.Models;

namespace TelstarCES.Services
{
    public interface IDataService
    {
        #region CITY

        Task<City> GetCity(ApplicationDbContext db, int cityId);

        Task<City[]> GetCities(ApplicationDbContext db);

        Task<bool> AddCity(ApplicationDbContext db, City city);

        Task<bool> UpdateCity(ApplicationDbContext db, City city);

        Task<bool> DeleteCity(ApplicationDbContext db, City city);

        #endregion

        #region CONNECTIONS

        Task<Connection> GetConnection(ApplicationDbContext db, int connectionId);

        Task<Connection[]> GetConnections(ApplicationDbContext db, int cityId);

        Task<Connection[]> GetConnections(ApplicationDbContext db);

        Task<bool> AddConnection(ApplicationDbContext db, Connection connection);

        Task<bool> UpdateConnection(ApplicationDbContext db, Connection connection);

        Task<bool> DeleteConnection(ApplicationDbContext db, Connection connection);

        #endregion

        #region

        Task<ParcelType> GetParcelType(ApplicationDbContext db, int parcelTypeId);

        Task<ParcelType[]> GetParcelTypes(ApplicationDbContext db);

        #endregion
    }
}
