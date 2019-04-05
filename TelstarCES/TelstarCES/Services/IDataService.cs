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

        Task<City> GetCity(int cityId);

        Task<City[]> GetCities();

        Task<bool> AddCity(City city);

        Task<bool> UpdateCity(City city);

        Task<bool> DeleteCity(City city);

        #endregion

        #region CONNECTIONS

        Task<Connection> GetConnection(int connectionId);

        Task<Connection[]> GetConnections(int cityId);

        Task<Connection[]> GetConnections();

        Task<bool> AddConnection(Connection connection);

        Task<bool> UpdateConnection(Connection connection);

        Task<bool> DeleteConnection(Connection connection);

        #endregion

        #region PARCEL_TYPES

        Task<ParcelType> GetParcelType(int parcelTypeId);

        Task<ParcelType[]> GetParcelTypes();

        #endregion

        #region ORDER

        Task<int> AddOrder(Order order);

        #endregion
    }
}
