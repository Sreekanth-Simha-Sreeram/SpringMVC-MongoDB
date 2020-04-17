using MongoDB.Driver;
using SpringMvc.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpringMvc.BusinessLayer.Interfaces
{
    public interface IUserServices
    {


        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(string name);
        Task AddUser(User model);
       // Task<bool> UpdatePrice(User model);
        Task<DeleteResult> RemoveUser(string name);
        Task<DeleteResult> RemoveAllUsers();
        Task<bool> UpdateUser (User userobj); //Jayanth Interface method to update user 
        //User Register(User user);
        //User GetUser(string UserId);
        //List<User> GetAllUser();
    }
}
