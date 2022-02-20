using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inlamningsuppgift;
using Inlamningsuppgift.Models.Entities;
using Inlamningsuppgift.Models;
using Inlamningsuppgift.Filtres;

namespace Inlamningsuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SqlContext _context;

        public UserController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        [UseApiKey]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {

            var items = new List<UserModel>();
            foreach (var i in await _context.Users.ToListAsync())
                items.Add(new UserModel(i.Id, i.FirstName, i.LastName, i.Email));
            return items;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [UseApiKey]
        public async Task<ActionResult<UserModel>> GetUserEntity(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);

            if (userEntity == null)
            {
                return NotFound();
            }

            return new UserModel(userEntity.Id, userEntity.FirstName, userEntity.LastName, userEntity.Email);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> PutUserEntity(int id, UserUpdateModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var userEntity = await _context.Users.FindAsync(id);
            userEntity.FirstName = model.FirstName;
            userEntity.LastName = model.LastName;
            userEntity.Email = model.Email;
            userEntity.Password = model.Password;

            _context.Entry(userEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [UseAdminApiKey]
        public async Task<ActionResult<UserModel>> PostUserEntity(UserCreateModel model)
        {
            if (await _context.Users.AnyAsync(x => x.Email == model.Email))
                return BadRequest();

            var userEntity = new UserEntity(model.FirstName, model.LastName, model.Email, model.Password);

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return new UserModel(userEntity.Id, userEntity.FirstName, userEntity.LastName, userEntity.Email);
        }





        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [UseAdminApiKey]
        public async Task<IActionResult> DeleteUserEntity(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEntityExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
