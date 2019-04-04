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
        #region CITY

        public async Task<City> GetCity(int cityId)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
            }
        }

        public async Task<City[]> GetCities()
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Cities.ToArrayAsync();
            }
        }

        public async Task<bool> AddCity(City city)
        {
            using (var db = new ApplicationDbContext())
            {
                await db.Cities.AddAsync(city);
                return await db.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateCity(City city)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Cities.Update(city);
                return await db.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteCity(City city)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Cities.Remove(city);
                return await db.SaveChangesAsync() > 0;
            }
        }

        #endregion CITY

        #region CONNECTION

        public async Task<Connection> GetConnection(int connectionId)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Connections.FirstOrDefaultAsync(c => c.ConnectionId == connectionId);
            }
        }

        public async Task<Connection[]> GetConnections(int cityId)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Connections.Where(c => c.City1.CityId == cityId || c.City2.CityId == cityId).ToArrayAsync();
            }
        }

        public async Task<Connection[]> GetConnections()
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Connections.ToArrayAsync();
            }
        }

        public async Task<bool> AddConnection(Connection connection)
        {
            using (var db = new ApplicationDbContext())
            {
                await db.Connections.AddAsync(connection);
                return await db.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateConnection(Connection connection)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Connections.Update(connection);
                return await db.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteConnection(Connection connection)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Connections.Remove(connection);
                return await db.SaveChangesAsync() > 0;
            }
        }

        #endregion

        #region PARCEL_TYPES

        public async Task<ParcelType> GetParcelType(int parcelTypeId)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.ParcelTypes.FirstOrDefaultAsync(pt => pt.ParcelTypeId == parcelTypeId);
            }
        }

        public async Task<ParcelType[]> GetParcelTypes()
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.ParcelTypes.ToArrayAsync();
            }
        }

        #endregion
    }
}
