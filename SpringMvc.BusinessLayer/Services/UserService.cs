using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SpringMvc.BusinessLayer.Interfaces;
using SpringMvc.Datalayer;
using SpringMvc.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvc.BusinessLayer.Services
{
    public class UserService : IUserServices
    {
        private readonly MongoRepository _repository = null;
        public UserService(IOptions<Settings> settings)
        {
            _repository = new MongoRepository(settings);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _repository.users.Find(x => true).ToListAsync();
        }

        public async Task<User> GetUser(string name)
        {
            var filter = Builders<User>.Filter.Eq("Name", name);
            return await _repository.users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddUser(User model)
        {
            //inserting data
            await _repository.users.InsertOneAsync(model);
        }

        //public Task<bool> UpdatePrice(User model)
        //{
        //    throw new NotImplementedException();
        //}

        //Jayanth written for updating user
        public async Task<bool> UpdateUser (User userobj)
        {

            var filter = Builders<User>.Filter.Eq("UserName", userobj.UserName);
            var user = _repository.users.Find(filter).FirstOrDefaultAsync();
            if (user.Result == null)
                return false;
            var update = Builders<User>.Update                                          
                                          .Set(x => x.Email, userobj.Email)
                                          .Set(x => x.Photo, userobj.Photo);                                          

            await _repository.users.UpdateOneAsync(filter, update);
            return true;
        }
        //Jayanth written for updating user

        public async Task<DeleteResult> RemoveUser(string name)
        {
            //Jayanth written to delete user by name
            var filter = Builders<User>.Filter.Eq("UserName", name);
            return await _repository.users.DeleteOneAsync(filter);
            //Jayanth written to delete user by name
        }

        public Task<DeleteResult> RemoveAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}

