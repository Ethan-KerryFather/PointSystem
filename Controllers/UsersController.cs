using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointSystem.Model;
using PointSystem.Model.Context;

namespace PointSystem.Controllers
{

    public class RegisterUser
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

    }
    [Route("api/[controller]")]
    [ApiController]
        public class UsersController : ControllerBase
        {
            private readonly UsersContext _context;

            public UsersController(UsersContext context)
            {
                _context = context;
            }

            // GET: api/Users
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
            {
            //return await _context.Users.ToListAsync();
            var linqResult = await _context.Users.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                phoneNumber = x.PhoneNumber,
                walletId = x.Points == null ? 0: x.Points.WalletId,
                balance = x.Points == null ? 0 : x.Points.Balance
            }).ToListAsync();

            return Ok(linqResult);
        
            }

            // GET: api/Users/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Users>> GetUsers(int id)
            {
                var users = await _context.Users.FindAsync(id);

                if (users == null)
                {
                    return NotFound();
                }

                return users;
            }

      
            // PUT: api/Users/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutUsers(int id, Users users)
            {
                if (id != users.Id)
                {
                    return BadRequest();
                }

                _context.Entry(users).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(id))
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

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns>사용자 등록</returns>
            // POST: api/Users
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost("/registerUser")]
            public async Task<ActionResult<Users>> PostUsers([FromBody]RegisterUser registerUser)
            {
                var users = new Users()
                {
                    Name = registerUser.Name,
                    PhoneNumber = registerUser.PhoneNumber,
                };
                _context.Users.Add(users);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUsers", new { id = users.Id }, users);
            }

            // DELETE: api/Users/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteUsers(int id)
            {
                var users = await _context.Users.FindAsync(id);
                if (users == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(users);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool UsersExists(int id)
            {
                return _context.Users.Any(e => e.Id == id);
            }
        }
    }
