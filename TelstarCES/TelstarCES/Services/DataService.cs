using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using TelstarCES.Data;
using TelstarCES.Data.Models;

namespace TelstarCES.Services
{
    public class DataService : IDataService
    {
        private readonly ApplicationDbContext _db;

        public DataService(ApplicationDbContext db)
        {
            _db = db;
        }

        #region CITY

        public async Task<City> GetCity(int cityId)
        {
            return await _db.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
        }

        public async Task<City> GetCity(string name)
        {
            return await _db.Cities.FirstOrDefaultAsync(c => string.Equals(c.CityName, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<City[]> GetCities()
        {
            return await _db.Cities.ToArrayAsync();
          
        }

        public async Task<bool> AddCity(City city)
        {
            await _db.Cities.AddAsync(city);
            return await _db.SaveChangesAsync() > 0;
   
        }

        public async Task<bool> UpdateCity(City city)
        {
            try
            {
                var updateCity = await _db.Cities.FirstOrDefaultAsync(c => c.CityId == city.CityId);
                if (updateCity == null)
                {
                    return false;
                }

                _db.Entry(updateCity).CurrentValues.SetValues(city);
                return await _db.SaveChangesAsync() > 0;            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCity(City city)
        {
            _db.Cities.Remove(city);
            return await _db.SaveChangesAsync() > 0;
        }

        #endregion CITY

        #region CONNECTION

        public async Task<Connection> GetConnection(int connectionId)
        {
            return await _db.Connections.FirstOrDefaultAsync(c => c.ConnectionId == connectionId);
        }

        public async Task<Connection[]> GetConnections(int cityId)
        {
            return await _db.Connections.Where(c => c.City1Id == cityId || c.City2Id == cityId).ToArrayAsync();
        }

        public async Task<Connection[]> GetConnections()
        {
            return await _db.Connections.ToArrayAsync();
        }

        public async Task<bool> AddConnection(Connection connection)
        {
            try
            {
                await _db.Connections.AddAsync(connection);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateConnection(Connection connection)
        {
            _db.Connections.Update(connection);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteConnection(Connection connection)
        {
            _db.Connections.Remove(connection);
            return await _db.SaveChangesAsync() > 0;
        }

        #endregion

        #region PARCEL_TYPES

        public async Task<ParcelType> GetParcelType(int parcelTypeId)
        {
            return await _db.ParcelTypes.FirstOrDefaultAsync(pt => pt.ParcelTypeId == parcelTypeId);
        }

        public async Task<ParcelType[]> GetParcelTypes()
        {
            return await _db.ParcelTypes.ToArrayAsync();
        }

        #endregion

        #region ORDER

        public async Task<int> AddOrder(Order order)
        {
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return order.OrderId;
        }

        #endregion
    }
}
