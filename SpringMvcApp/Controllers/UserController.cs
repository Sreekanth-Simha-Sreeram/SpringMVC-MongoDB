using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SpringMvc.BusinessLayer.Interfaces;

using SpringMvc.Entities;

namespace SpringMvcApp.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserServices _userService;
        public UserController(IUserServices userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("api/user")]
        public Task<IEnumerable<User>> Get()
        {
            return _userService.GetAllUsers();
        }

        [HttpPost]
        [Route("api/user")]
        public async Task<IActionResult> Post(User model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.UserName))
                    return BadRequest("Please enter UserName");
                else if (string.IsNullOrWhiteSpace(model.Password))
                    return BadRequest("Please enter Password");
                else if (string.IsNullOrWhiteSpace(model.ConfirmPassword))
                    return BadRequest("Please enter ConfirmPassword");
                else if (string.IsNullOrWhiteSpace(model.Email))
                    return BadRequest("Please enter Email");


                await _userService.AddUser(model);
                return Ok("Your product has been added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        
        [HttpGet]
        [Route("api/user/getByName")]
        public async Task<IActionResult> GetByCategory(string name)
        {
            try
            {
                var user = await _userService.GetUser(name);
                if (user == null)
                {
                    return Json("No user found!");
                }
               // return Json(user);
                return this.Json(user);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());

            }
        }



        //Jayanth Changed to make sure data insertion is done (06-04-2020)
        [HttpPost]
        [Route("api/user/addUser")]
        public async Task<IActionResult> AddUser ([FromBody]User model) //Written From Body tag for fetching the values from body of Postman.
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.UserName))
                    return BadRequest("UserName missing");

                await _userService.AddUser(model);

                return Ok ("User Successfully Inserted");
            }
            catch (Exception)
            {
                return BadRequest ("User Insertion Failed");
            }           
        }
        //Jayanth Changed to make sure data insertion is done (06-04-2020)


        //Jayanth Changed to make sure data insertion is done (06-04-2020)
        [HttpPut]
        [Route("api/user/updateUser")]
        public async Task<IActionResult> UpdateUser ([FromBody]User userobj) //Written From Body tag for fetching the values from body of Postman.
        {
            try
            {
                var result = await _userService.UpdateUser(userobj);
                if (result)
                {
                    return Ok ("User updated successfully");
                }
                return BadRequest("User Updation Failed");
            }
            catch (Exception)
            {
                return BadRequest("User Updation Failed");
            }
        }
        //Jayanth Changed to make sure data insertion is done (06-04-2020)

        [HttpDelete]
        [Route("api/user/deleteUser")]
        public async Task<IActionResult> DeleteUser (string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("user name missing");
                await _userService.RemoveUser(name);
                return Ok("Your user has been deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete]
        [Route("api/user/deleteAll")]
        public IActionResult DeleteAll()
        {
            try
            {
                _userService.RemoveAllUsers();
                return Ok("Your all user has been deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }



    }
}