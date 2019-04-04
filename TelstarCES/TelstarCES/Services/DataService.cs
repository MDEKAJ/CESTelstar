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

        public async Task<City> GetCity(ApplicationDbContext db, int cityId)
        {
            return await db.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
          
        }

        public async Task<City[]> GetCities(ApplicationDbContext db)
        {
            return await db.Cities.ToArrayAsync();
          
        }

        public async Task<bool> AddCity(ApplicationDbContext db, City city)
        {
            await db.Cities.AddAsync(city);
            return await db.SaveChangesAsync() > 0;
            
        }

        public async Task<bool> UpdateCity(ApplicationDbContext db, City city)
        {
            db.Cities.Update(city);
            return await db.SaveChangesAsync() > 0;            
        }

        public async Task<bool> DeleteCity(ApplicationDbContext db, City city)
        {
            db.Cities.Remove(city);
            return await db.SaveChangesAsync() > 0;
        }

        #endregion CITY

        #region CONNECTION

        public async Task<Connection> GetConnection(ApplicationDbContext db, int connectionId)
        {
            return await db.Connections.FirstOrDefaultAsync(c => c.ConnectionId == connectionId);
        }

        public async Task<Connection[]> GetConnections(ApplicationDbContext db, int cityId)
        {
            return await db.Connections.Where(c => c.City1Id == cityId || c.City2Id == cityId).ToArrayAsync();
        }

        public async Task<Connection[]> GetConnections(ApplicationDbContext db)
        {
            return await db.Connections.ToArrayAsync();
        }

        public async Task<bool> AddConnection(ApplicationDbContext db, Connection connection)
        {
            await db.Connections.AddAsync(connection);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateConnection(ApplicationDbContext db, Connection connection)
        {
            db.Connections.Update(connection);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteConnection(ApplicationDbContext db, Connection connection)
        {
            db.Connections.Remove(connection);
            return await db.SaveChangesAsync() > 0;
        }

        #endregion

        #region PARCEL_TYPES

        public async Task<ParcelType> GetParcelType(ApplicationDbContext db, int parcelTypeId)
        {
            return await db.ParcelTypes.FirstOrDefaultAsync(pt => pt.ParcelTypeId == parcelTypeId);
        }

        public async Task<ParcelType[]> GetParcelTypes(ApplicationDbContext db)
        {
            return await db.ParcelTypes.ToArrayAsync();
        }

        #endregion
    }
}
