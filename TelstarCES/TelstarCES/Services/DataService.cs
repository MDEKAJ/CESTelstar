using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;
using TelstarCES.Data;
using TelstarCES.Data.Models;

namespace TelstarCES.Services
{
    public class DataService : IDataService
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly ApplicationDbContext _db;

        public DataService(ApplicationDbContext db)
        {
            _db = db;
        }

        #region CITY

        public async Task<City> GetCity(int cityId)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                return await _db.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<City> GetCity(string name)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                return await _db.Cities.FirstOrDefaultAsync(c =>
                    string.Equals(c.CityName, name, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<City[]> GetCities()
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                return await _db.Cities.ToArrayAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> AddCity(City city)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                await _db.Cities.AddAsync(city);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> UpdateCity(City city)
        {
            await _semaphoreSlim.WaitAsync();
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
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> DeleteCity(City city)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                _db.Cities.Remove(city);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        #endregion CITY

        #region CONNECTION

        public async Task<Connection> GetConnection(int connectionId)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                return await _db.Connections.FirstOrDefaultAsync(c => c.ConnectionId == connectionId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<Connection[]> GetConnections(int cityId)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                return await _db.Connections.Where(c => c.City1Id == cityId || c.City2Id == cityId).ToArrayAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<Connection[]> GetConnections()
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                return await _db.Connections.ToArrayAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> AddConnection(Connection connection)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                await _db.Connections.AddAsync(connection);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> UpdateConnection(Connection connection)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                _db.Connections.Update(connection);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> DeleteConnection(Connection connection)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                _db.Connections.Remove(connection);
                return await _db.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        #endregion

        #region PARCEL_TYPES

        public async Task<ParcelType> GetParcelType(int parcelTypeId)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                return await _db.ParcelTypes.FirstOrDefaultAsync(pt => pt.ParcelTypeId == parcelTypeId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<ParcelType[]> GetParcelTypes()
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                return await _db.ParcelTypes.ToArrayAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        #endregion

        #region ORDER

        public async Task<int> AddOrder(Order order)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();
                return order.OrderId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        #endregion
    }
}
