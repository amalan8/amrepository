using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2Service.Models;

namespace Project2Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<User> users = new List<User>();
        public static int currentId = 101;

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET: api/Users/101
        [HttpGet("{id}", Name = "Get")]
        public User Get(int id)
        {
            return users.Where(t => t.Id == id).FirstOrDefault();
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }

            value.Id = currentId++;
            value.DateAdded = DateTime.Now;
            //using this just to get the idea of hashing as a beginner.
            value.Password = value.Password.GetHashCode().ToString();
            users.Add(value);

            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        // PUT: api/Users/101
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
        {
            User user = users.FirstOrDefault(t => t.Id == id);

            if (user != null)
            {
                user.Id = id;
                user.Email = value.Email;
                user.Password = value.Password.GetHashCode().ToString();
                //just seeing what this does. 
                //int hashedPassword = user.Password.GetHashCode();


                return Ok(user);
            }
            else
            {
                return NotFound();
            }


        }

        // DELETE: api/Users/101
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = users.Where(t => t.Id == id).FirstOrDefault();

            users.Remove(user);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }
    }
}